using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Storage;
using UnityEngine;
using System.Threading;
using UnityEngine.Networking;
using System.Net;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    public class FirebaseCloudStorage : iCloudStorage
    {
        #region private
        private static bool _inited;
        private bool _initing;
        private FirebaseStorage _storage;
        #endregion

        #region iCloudStorage
        [ShowInInspector] public bool inited => _inited;

        public async Task<(Texture2D, Exception)> DownloadTexture(string path, Action<float> onProgressChange = null, CancellationToken cancel = default)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(path.IsNullOrEmpty())
                    return (null,new System.ArgumentNullException("Path"));

                var reference   = _storage.GetReference(path);
                var url         = await reference.GetDownloadUrlAsync();
                var request     = UnityWebRequestTexture.GetTexture(url);
                var async       = request.SendWebRequest();
                var status      = default(HttpStatusCode);
                var progress    = 0f;

                while(true)
                {
                    onProgressChange?.Invoke(progress);
                    await new WaitUntil(()=> progress != async.progress);
                    progress = async.progress;              
                    if(async.progress >= 1f)
                    {
                        onProgressChange?.Invoke(progress);
                        break;
                    } 
                }
                
                await new WaitUntil(()=> async.isDone);

                if (request.isNetworkError)
                {
                    status = HttpStatusCode.RequestTimeout;
                    return (null,new HttpListenerException((int)status,status.ToString()));
                }
                else
                {
                    status = (HttpStatusCode)request.responseCode;
                    switch(status)
                    {
                        case HttpStatusCode.OK:
                        {
                            var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                            //DownloadHandlerTexture.GetContent(request);
                            return (texture,null);
                        }

                        default:{
                            "{0} - {1}".LogErrorFormat(status, url);
                            return (null,new HttpListenerException((int)status,status.ToString()));
                        }
                    }
                }
            } 
            catch (System.Exception ex) 
            {
                return (null,ex);
            }
        }
        public async Task<(byte[],Exception)> DownloadFile(string path, Action<float> progress = null,CancellationToken cancel = default(CancellationToken))
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(path.IsNullOrEmpty())
                    return (null,new System.ArgumentNullException("Path"));

                var reference = _storage.GetReference(path);

                var bytes = await reference.GetBytesAsync(long.MaxValue,new StorageProgress<DownloadState>(state =>
                {
                    var percent = state.BytesTransferred / (float)state.TotalByteCount;
                    progress?.Invoke(percent);
                }),cancel);

                return (bytes,null);
            } 
            catch (StorageException ex) 
            {
                return (null,ex);
            }
        }
        public async Task<Exception> DownloadFile(string path, string saveLocalFile, Action<float> progress = null,CancellationToken cancel = default(CancellationToken))
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("Path");
    
                var reference = _storage.GetReference(path);
                await reference.GetFileAsync(saveLocalFile,new StorageProgress<DownloadState>(state =>
                {
                    var percent = state.BytesTransferred / (float)state.TotalByteCount;
                    progress?.Invoke(percent);
                }),cancel);

                return null;
            } 
            catch (StorageException ex) 
            {
                //ex.Message.LogError();
                return ex;
            }
        }
        public async Task<Exception> DownloadStream(string path,Action<System.IO.Stream> stream,Action<float> progress = null,CancellationToken cancel = default(CancellationToken))
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("Path");
    
                var reference = _storage.GetReference(path);
                await reference.GetStreamAsync(stream,new StorageProgress<DownloadState>(state =>
                {
                    var percent = state.BytesTransferred / (float)state.TotalByteCount;
                    progress?.Invoke(percent);
                }),cancel);

                return null;
            } 
            catch (StorageException ex) 
            {
                //ex.Message.LogError();
                return ex;
            }
        }
        public async Task<Exception> UploadFile(byte[] bytes, string path, Action<float> progress = null,CancellationToken cancel = default(CancellationToken))
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(bytes.IsNullOrEmpty())
                    return new System.ArgumentNullException("bytes");

                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("path");
    
                var reference   = _storage.GetReference(path);
                await reference.PutBytesAsync(bytes,progressHandler : new StorageProgress<UploadState>(state =>
                {
                    var percent = state.BytesTransferred / (float)state.TotalByteCount;
                    progress?.Invoke(percent);
                }),cancelToken : cancel);

                return null;
            } 
            catch (StorageException ex) 
            {
                //ex.Message.LogError();
                return ex;
            }
        }
        public async Task<Exception> UploadFile(string localFilePath, string path, Action<float> progress = null,CancellationToken cancel = default(CancellationToken))
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(!localFilePath.FileExist())
                    return new System.ArgumentNullException("localFilePath");
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("Path");
    
                var reference = _storage.GetReference(path);
                await reference.PutFileAsync(localFilePath,progressHandler : new StorageProgress<UploadState>(state =>
                {
                    var percent = state.BytesTransferred / (float)state.TotalByteCount;
                    progress?.Invoke(percent);
                }),cancelToken: cancel);

                return null;
            } 
            catch (StorageException ex) 
            {
                //ex.Message.LogError();
                return ex;
            }
        }
        public async Task<Exception> RemoveFile(string path)
        {   
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("Path");

                await _storage.GetReference(path).DeleteAsync();
                
                return null;
            } 
            catch (StorageException ex) 
            {
                //ex.Message.LogError();
                return ex;
            }
        }
        #endregion

        #region Constructor
        public FirebaseCloudStorage()
        {
            Init(null);
        }
        public FirebaseCloudStorage(string url)
        {
            Init(url);
        }
        #endregion

        #region methods
        private async void Init(string storage)
        {
            if(_inited || _initing)
                return;

            _initing = true;
            var status = await FirebaseDependencies.CheckAndFixDependencies();

            switch(status)
            {
                case DependencyStatus.Available:
                {   
                    _storage = storage.IsNullOrEmpty()? FirebaseStorage.DefaultInstance : FirebaseStorage.GetInstance(storage);
                    _initing = false;
                    _inited = true;
                    break;
                }

                default:
                {
                    _initing = false;
                    _inited = false;
                    break;
                }
            }
        }
        #endregion
    }
}
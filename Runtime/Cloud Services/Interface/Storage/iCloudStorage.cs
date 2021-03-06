#if ODIN_INSPECTOR 
using System;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Evesoft.CloudService
{
    public interface ICloudStorage
    {
        Task<(Texture2D,Exception)> DownloadTexture(string path,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<(byte[],Exception)> DownloadFile(string path,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<Exception> DownloadFile(string path,string saveLocalFile,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<Exception> DownloadStream(string path,Action<System.IO.Stream> stream,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<Exception> UploadFile(byte[] bytes,string path,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<Exception> UploadFile(string localFilePath,string path,Action<float> progress = null,CancellationToken cancel = default(CancellationToken));
        Task<Exception> RemoveFile(string path);
    }
}
#endif
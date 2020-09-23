using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Evesoft.Cache;

namespace Evesoft.Http
{
    public static class HttpRequestTexture2D
    {
        public static async Task<(Texture2D,Exception)> GetTexture(string url,string key,bool cached = false,bool mipmap = false,Action<float> progressHandle = null)
        {           
            try 
            {
                if(url.IsNullOrEmpty())
                    return (null,new ArgumentNullException(nameof(url)));
    
                if(key.IsNullOrEmpty())
                    return (null,new ArgumentNullException(nameof(key)));
    
                //Get from cache
                if(cached)
                {
                    var cache = Texture2DCache.defaultInstance.GetCache(url,key);
                    if(!cache.IsNull())
                    {
                        if(cache.url == url)
                        {
                            //onComplete?.Invoke((cache.data,null));
                            return (cache.data,null);
                        }
                        else
                        {
                            Texture2DCache.defaultInstance.RemoveCache(cache); 
                        }   
                    }
                }

                var request  = UnityWebRequestTexture.GetTexture(url);
                var isDone   = false;
                var progress = 0f;
                var async    = request.SendWebRequest();
                    progressHandle?.Invoke(progress);
                do
                {
                    await new WaitForFixedUpdate();
                    if(!request.isNetworkError && progress != async.progress)
                    {
                        progress = async.progress;
                        progressHandle?.Invoke(progress);
                    }

                    if(request.isNetworkError || isDone)
                        break;

                }while(!async.isDone);
                
                var status = (HttpStatusCode)request.responseCode;
                if (request.isNetworkError)
                {
                    return(null, new WebException(HttpStatusCode.RequestTimeout.ToString()));
                }
                else if(status == HttpStatusCode.OK)
                {
                    var texture = DownloadHandlerTexture.GetContent(request); 

                    if(mipmap)
                    {
                        var temp = texture;
                        var pixels = texture.GetPixels();
                        texture = new Texture2D(texture.width,texture.height,texture.format,true);
                        texture.SetPixels(pixels);
                        texture.Apply();
                        temp.Destroy();
                        temp = null;
                    }

                    if(!texture.IsNull())
                        texture.name = key;
    
                    if(cached)
                        Texture2DCache.defaultInstance.AddCache(new Texture2DCacheData(url,key,texture));
                        
                    //onComplete?.Invoke((texture,null));
                    return(texture,null);
                }
                else
                {
                    return (null,new WebException(status.ToString()));
                }
            } 
            catch (Exception ex) 
            {
                ex.Message.LogError();
                return(null,ex);
            }
        }
    }
}
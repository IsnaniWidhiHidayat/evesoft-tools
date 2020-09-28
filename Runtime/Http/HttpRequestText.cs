#if ODIN_INSPECTOR 
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Evesoft.Http
{
    public static class HttpRequestText
    {
        public static async Task<(string,Exception)> GetText(string url)
        {
            if(url.IsNullOrEmpty())
                return (null, new ArgumentNullException(nameof(url)));

            var request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
                        
            if (request.isNetworkError)
            {
                return(null,new WebException(HttpStatusCode.RequestTimeout.ToString()));
            }
            else
            {
                var status = (HttpStatusCode)request.responseCode;
                switch(status)
                {
                    case HttpStatusCode.OK:
                    {
                        return (request.downloadHandler.text,null);
                    }

                    default:
                    {    
                        return(null,new WebException(status.ToString()));
                    }
                } 
            }
        }
    }
}
#endif
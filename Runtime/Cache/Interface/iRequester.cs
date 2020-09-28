#if ODIN_INSPECTOR 
using System;
using System.Net;

namespace Evesoft.Cache
{
    public interface iRequester<T> 
    {
        event Action<iRequester<T>,float> onProgressChange;
        event Action<iRequester<T>> onComplete;
        event Action<iRequester<T>,Exception> onFailed;
        
        string url{get;}
        //bool caching{get;}
        T result{get;}

        void SendRequest();
    }
}


#endif
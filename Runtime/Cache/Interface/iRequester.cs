#if ODIN_INSPECTOR 
using System;
using System.Net;

namespace Evesoft.Cache
{
    public interface IRequester<T> 
    {
        event Action<IRequester<T>,float> onProgressChange;
        event Action<IRequester<T>> onComplete;
        event Action<IRequester<T>,Exception> onFailed;
        
        string url{get;}
        //bool caching{get;}
        T result{get;}

        void SendRequest();
    }
}


#endif
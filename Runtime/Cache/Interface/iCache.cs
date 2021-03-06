#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;

namespace Evesoft.Cache
{
    public interface ICache<T>
    {
        event Action<ICache<T>> onLoaded;
        event Action<ICache<T>> onSaved;

        List<T> cached{get;}
        T GetCache(string url,string key);
        void AddCache(T cache);
        void RemoveCache(T cache);
        void UpdateCache(T cache);
        void Save();
        void Load();
    }
}
#endif
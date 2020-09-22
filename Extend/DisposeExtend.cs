using System;
using System.Collections.Generic;

namespace EveSoft
{
    public static class DisposeExtend
    {
        public static void Dispose<T>(this IList<T> list)
        {
            if(list.IsNullOrEmpty())
                return;

            foreach (var item in list)
                if(!item.IsNull())
                    item.Dispose();
        }
        
        public static void Dispose(this object obj)
        {
            if(obj.IsNull())
                return;

            var disposable = obj as IDisposable;
            if(!disposable.IsNull())
            {
                disposable.Dispose();
                return;
            }

            var unityObject = obj as UnityEngine.Object;
                unityObject?.Destroy();
        }  
    }
}
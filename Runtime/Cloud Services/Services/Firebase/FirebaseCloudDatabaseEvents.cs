#if ODIN_INSPECTOR 
#if FIREBASE_REALTIME_DATABASE
using System;

namespace Evesoft.CloudService.Firebase
{
    internal class FirebaseCloudDatabaseEvents : ICloudDatabaseEvents,IDisposable
    {
        #region iCloudDatabaseEvents
        public event Action<string, object> onDataAdded;
        public event Action<string, object> onDataChange;
        public event Action<string, object> onDataRemoved;
        #endregion

        internal void OnDataAdded(string key, object value)
        {
            onDataAdded?.Invoke(key,value);
        }
        internal void OnDataChange(string key, object value)
        {
            onDataChange?.Invoke(key,value);
        }
        internal void OnDataRemoved(string key, object value)
        {
            onDataRemoved?.Invoke(key,value);
        }
    
        #region IDisposable
        public void Dispose()
        {
            onDataAdded = null;
            onDataChange = null;
            onDataRemoved = null;
        }
        #endregion
    }
}
#endif
#endif
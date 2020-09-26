#if FIREBASE_REALTIME_DATABASE
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    internal class FirebaseCloudDatabaseConfig : iCloudDatabaseConfig
    {
        #region const
        public const string DB = nameof(DB);
        public const string REF = nameof(REF);
        #endregion

        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iAdsConfig
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region constructor
        public FirebaseCloudDatabaseConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(CloudService)] = CloudDatabaseType.FirebaseRealtimeDatabase;
        }
        public FirebaseCloudDatabaseConfig(string db):this()
        {
            _configs[DB] = db;
        }
        #endregion
    }
}
#endif
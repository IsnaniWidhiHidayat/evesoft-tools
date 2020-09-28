#if ODIN_INSPECTOR 
#if FIREBASE_REMOTE_CONFIG || FIREBASE_REALTIME_DATABASE
using System.Collections.Generic;

namespace Evesoft.CloudService.Firebase
{
    public class FirebaseCloudRemoteSetting : iCloudRemoteSetting
    {
        #region const
        internal const string TYPE = nameof(TYPE);
        internal const string DEVMODE = nameof(DEVMODE);
        #endregion

        #region private
        private IDictionary<string, object> _configs;
        #endregion

        #region iCloudRemoteSetting
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region contructor
        public FirebaseCloudRemoteSetting(FirebaseCloudRemoteConfigType type,bool devMode)
        {
            _configs                                        = new Dictionary<string,object>();
            _configs[nameof(CloudService)]                  = CloudRemoteConfigType.FirebaseRemoteConfig;
            _configs[TYPE]      = type;
            _configs[DEVMODE]   = devMode;
        }
        #endregion
    }
}
#endif
#endif
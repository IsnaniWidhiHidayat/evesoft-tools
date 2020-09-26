using System.Collections.Generic;

namespace Evesoft.CloudService.Firebase
{
    public class FirebaseCloudRemoteSetting : iCloudRemoteSetting
    {
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
        public FirebaseCloudRemoteSetting(FirebaseCloudRemoteConfigType type)
        {
            _configs                                        = new Dictionary<string,object>();
            _configs[nameof(CloudService)]                  = CloudRemoteConfigType.FirebaseRemoteConfig;
            _configs[nameof(FirebaseCloudRemoteConfigType)] = type;
        }
        #endregion
    }
}
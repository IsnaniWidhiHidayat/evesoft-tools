#if UNITY_REMOTE_CONFIG
using System.Collections.Generic;

namespace Evesoft.CloudService.UnityRemoteConfig
{
    public class UnityRemoteSetting : iCloudRemoteSetting
    {
        #region const
        internal const string USER = nameof(USER);
        internal const string APP = nameof(APP);
        internal const string USERID = nameof(USERID);
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
    
        #region methods
        internal void SetUserAttribute<T>(T value) where T: struct
        {
            _configs[USER] = value;
        }
        public void SetAppAttribute<T>(T value) where T: struct
        {
            _configs[APP] = value;
        }
        #endregion

        #region contructor
        public UnityRemoteSetting()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(CloudService)] = CloudRemoteConfigType.UnityRemoteConfig;
        }
        public UnityRemoteSetting(string userCustomId):this()
        {
            if(!userCustomId.IsNullOrEmpty())
                _configs[USERID] = userCustomId;
        }
        #endregion
    }
}
#endif
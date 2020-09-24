using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.GoogleSignIn
{
    [Serializable,HideReferenceObjectPicker]
    public class GoogleAuthConfig : iCloudAuthConfig
    {
        #region const
        internal const string WEB_CLIENT_ID = nameof(WEB_CLIENT_ID);
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
        public GoogleAuthConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(CloudService)] = CloudAuthType.GoogleSignIn;        
        }
        public GoogleAuthConfig(string webClientID):this()
        {
            _configs[WEB_CLIENT_ID] = webClientID;
        }
        #endregion
    }
}
#if ODIN_INSPECTOR 
#if PLAYSERVICE_AUTH
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.GooglePlayService
{
    [Serializable,HideReferenceObjectPicker]
    internal class GooglePlayServiceAuthConfig : ICloudAuthConfig
    {
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
        public GooglePlayServiceAuthConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(CloudService)] = CloudAuthType.GooglePlayService;
        }
        #endregion
    }
}
#endif
#endif
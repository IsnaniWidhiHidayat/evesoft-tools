using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    public class FirebaseCloudAuthConfig : iCloudAuthConfig
    {
        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iAdsConfig
        public IDictionary<string, object> configs
        {
            get
            {
                if(_configs.IsNull())
                {
                    _configs = new Dictionary<string,object>();
                }

                return _configs;
            }
        }
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion
    }
}
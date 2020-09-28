#if ODIN_INSPECTOR 
#if GOOGLE_AUTH
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.CloudService.GoogleSignIn
{
    [Serializable,HideReferenceObjectPicker]
    internal class GoogleAuthConfig : iCloudAuthConfig
    {
        #region const
        internal const string WEB_CLIENT_ID = nameof(WEB_CLIENT_ID);
        #endregion

        #region field
        [SerializeField,HideLabel,SuffixLabel(nameof(webClientID),true),Multiline(3)]
        private string webClientID;
        #endregion

        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iCloudAuthConfig
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
            _configs[WEB_CLIENT_ID] = this.webClientID  = webClientID;
        }
        #endregion
    }
}
#endif
#endif
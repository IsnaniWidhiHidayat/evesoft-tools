#if GOOGLE_AUTH
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Evesoft.CloudService.GoogleSignIn
{
    [CreateAssetMenu(menuName = nameof(Evesoft) +"/"+ nameof(Evesoft.CloudService) +"/" + nameof(GoogleAuthConfig),fileName = nameof(GoogleAuthConfig))]
    public class GoogleAuthConfigAsset : SerializedScriptableObject, iCloudAuthConfig
    {
        [SerializeField,HideInInspector]
        private GoogleAuthConfig _config;

        [ShowInInspector,HideLabel]
        private GoogleAuthConfig config{
            get
            {
                if(_config.IsNull())
                    _config = new GoogleAuthConfig();

                return _config;
            }

            set{
                _config = value;
            }
        }

        #region iAdsConfig
        public T GetConfig<T>(string key)
        {
            return config.GetConfig<T>(key);
        }
        #endregion
    }
}
#endif
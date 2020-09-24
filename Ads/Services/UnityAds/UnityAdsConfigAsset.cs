using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Evesoft.Ads.UnityAds
{
    [CreateAssetMenu(menuName = nameof(Evesoft) +"/"+ nameof(Evesoft.Ads) +"/" + nameof(UnityAdsConfig),fileName = nameof(UnityAdsConfig))]
    public class AdmobConfigAsset : SerializedScriptableObject, iAdsConfig
    {
        [SerializeField,HideInInspector]
        private UnityAdsConfig _config;

        [ShowInInspector,HideLabel]
        private UnityAdsConfig config{
            get
            {
                if(_config.IsNull())
                    _config = new UnityAdsConfig();

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
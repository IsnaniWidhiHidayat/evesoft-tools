#if ADMOB
using UnityEngine;
using Sirenix.OdinInspector;
namespace Evesoft.Ads.Admob
{
    [CreateAssetMenu(menuName = nameof(Evesoft) +"/"+ nameof(Evesoft.Ads) +"/" + nameof(AdmobConfig),fileName = nameof(AdmobConfig))]
    public class AdmobConfigAsset : SerializedScriptableObject, iAdsConfig
    {
        [SerializeField,HideInInspector]
        private AdmobConfig _config;

        [ShowInInspector,HideLabel]
        private AdmobConfig config{
            get
            {
                if(_config.IsNull())
                    _config = new AdmobConfig();

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
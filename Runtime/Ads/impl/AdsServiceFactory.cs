#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.Ads
{
    public static class AdsServiceFactory
    {
        private static IDictionary<AdsType,iAdsService> adServices = new Dictionary<AdsType, iAdsService>();

        public static iAdsService Create(iAdsConfig config)
        {
            if(config.IsNull())
                return null;

            var type = config.GetConfig<AdsType>(nameof(Ads));
            if(adServices.ContainsKey(type))
                return adServices[type];

            switch(type)
            {
                #if ADMOB
                case AdsType.Admob:
                {
                    return adServices[type] = new Admob.Admob(config);
                }
                #endif

                #if UNITY_ADS
                case AdsType.UnityAds:
                {
                    return adServices[type] = new UnityAds.UnityAds(config);
                }
                #endif
            
                default:
                {
                    "Service Unavailable".LogError();
                    return null;
                }
            }
        }
        public static iAdsService Get(AdsType type)
        {
            if(adServices.ContainsKey(type))
                return adServices[type];

            return null;
        }
        public static async Task<iAdsService> GetAsync(AdsType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
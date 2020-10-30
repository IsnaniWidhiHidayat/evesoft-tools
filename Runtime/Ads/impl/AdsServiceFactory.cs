#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.Ads
{
    public static class AdsServiceFactory
    {
        private static IDictionary<AdsType,IAdsService> adServices = new Dictionary<AdsType, IAdsService>();

        public static IAdsService Create(IAdsConfig config)
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
        public static IAdsService Get(AdsType type)
        {
            if(adServices.ContainsKey(type))
                return adServices[type];

            return null;
        }
        public static async Task<IAdsService> GetAsync(AdsType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
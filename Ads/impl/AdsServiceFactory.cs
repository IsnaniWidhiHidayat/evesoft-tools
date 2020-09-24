using System.Collections.Generic;

namespace Evesoft.Ads
{
    public static class AdsServiceFactory
    {
        private static IDictionary<AdsType,iAdsService> adServices = new Dictionary<AdsType, iAdsService>();

        public static iAdsService CreateService(iAdsConfig config)
        {
            if(config.IsNull())
                return null;

            var type = config.GetConfig<AdsType>(nameof(Ads));
            if(adServices.ContainsKey(type))
                return adServices[type];

            switch(type)
            {
                case AdsType.Admob:
                {
                    return adServices[type] = new Admob.Admob(config);
                }

                case AdsType.UnityAds:
                {
                    return adServices[type] = new UnityAds.UnityAds(config);
                }
            
                default:
                {
                    "Service Unavailable".LogError();
                    return null;
                }
            }
        }
    }
}
using System.Collections.Generic;

namespace Evesoft.Ads
{
    public static class AdServiceFactory
    {
        private static IDictionary<string,iAdsService> adServices = new Dictionary<string, iAdsService>();

        public static iAdsService CreateService(iAdsConfig config)
        {
            if(config.IsNull())
                return null;

            var type = config.GetConfig<string>(nameof(Ads));
            if(type.IsNullOrEmpty())
                return null;

            switch(type)
            {
                case nameof(Admob):
                {
                    return adServices[nameof(Admob)] = new Admob.Admob(config);
                }

                case nameof(UnityAds):
                {
                    return adServices[nameof(UnityAds)] = new UnityAds.UnityAds(config);
                }
            }

            return null;
        }
    }
}
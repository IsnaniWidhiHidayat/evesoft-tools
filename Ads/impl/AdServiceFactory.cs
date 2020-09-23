using System.Collections.Generic;

namespace Evesoft.Ads
{
    public static class AdServiceFactory
    {
        private static IDictionary<string,iAdsService> adServices = new Dictionary<string, iAdsService>();

        public static iAdsService CreateAdmobService(iAdsConfig config)
        {
            if(adServices.ContainsKey(nameof(Admob)))
                return adServices[nameof(Admob)];

            return adServices[nameof(Admob)] = new Admob.Admob(config);
        }
    }
}
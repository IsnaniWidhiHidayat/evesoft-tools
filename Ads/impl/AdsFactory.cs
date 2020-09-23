using System.Collections.Generic;

namespace EveSoft.Ads
{
    public static class AdsFactory
    {
        private static Dictionary<AdsProvider,iAdsService> adServices = new Dictionary<AdsProvider, iAdsService>();

        public static iAdsService Create(AdsProvider provider,iAdsConfig config)
        {
            switch(provider)
            {
                case AdsProvider.GoogleAdmob:
                {
                    if(adServices.ContainsKey(provider))
                        return adServices[provider];

                    var ads = new Admob.Admob(config);
                    return adServices[provider] = ads;
                }

                default:
                {
                    return null;
                }
            }
        }
    }
}
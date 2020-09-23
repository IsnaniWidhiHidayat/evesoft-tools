using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Ads
{
    public static class AdConfigFactory
    {
        public static iAdsConfig CreateAdmobConfig(string bannerID,string interstitialID,string rewardID,bool tagForChild,Admob.AdPosition bannerPosition,Vector2Int customPosition,string[] keywords)
        {
            var config = new Admob.AdmobConfig(bannerID,interstitialID,rewardID,tagForChild,bannerPosition,customPosition,keywords);
            config.configs[nameof(Ads)] = nameof(Admob);
            return config;
        }
        public static iAdsConfig CreateUnityAdsConfig(UnityAds.UnityAdsStore store,string playstoreID,string appleStoreID,string bannerID,string interstitialID,string rewardID,bool testMode)
        {
            var config = new UnityAds.UnityAdsConfig(store,playstoreID,appleStoreID,bannerID,interstitialID,rewardID,testMode);
            config.configs[nameof(Ads)] = nameof(UnityAds);
            return config;
        }
    }
}


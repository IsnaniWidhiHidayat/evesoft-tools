using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Ads
{
    public static class AdsConfigFactory
    {
        public static iAdsConfig CreateAdmobConfig(string bannerID,string interstitialID,string rewardID,bool tagForChild,Admob.AdsGender gender,Admob.AdPosition bannerPosition,Vector2Int customPosition,string[] keywords)
        {
            return new Admob.AdmobConfig(bannerID,interstitialID,rewardID,tagForChild,gender,bannerPosition,customPosition,keywords);
        }
        public static iAdsConfig CreateUnityAdsConfig(string playstoreID,string appleStoreID,string bannerID,string interstitialID,string rewardID,bool testMode)
        {
            return new UnityAds.UnityAdsConfig(playstoreID,appleStoreID,bannerID,interstitialID,rewardID,testMode);
        }
    }
}


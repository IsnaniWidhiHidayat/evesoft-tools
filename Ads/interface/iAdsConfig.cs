using UnityEngine;

namespace EveSoft.Ads
{
    public interface iAdsConfig
    {
        string appId{get;set;}      
        string bannerID{get;set;}       
        string interstitialID{get;}
        string rewardID{get;set;}
        bool tagForChild{get;set;}
        AdsGender gender{get;set;}
        AdPosition bannerPosition{get;set;}
        Vector2Int customPosition{get;set;}
        string[] keywords{get;set;}
    }
}


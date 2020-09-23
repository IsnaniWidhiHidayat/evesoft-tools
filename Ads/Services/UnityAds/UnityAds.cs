using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Evesoft.Ads.UnityAds
{
    public class UnityAds : iAdsService
    {
        #region iAdsService
        public bool isBannerLoaded => false;
        public bool isRewardLoaded =>false;
        public bool isInterstitialLoaded =>false; 

        public event Action<iAdsService> onBannerLeaveApp;
        public event Action<iAdsService> onBannerOpening;
        public event Action<iAdsService> onBannerClosed;
        public event Action<iAdsService> onBannerFailed;
        public event Action<iAdsService> onBannerLoad;
        public event Action<iAdsService> onInterstitialOpening;
        public event Action<iAdsService> onInterstitialRequested;
        public event Action<iAdsService> onInterstitialLoaded;
        public event Action<iAdsService> onInterstitialFailed;
        public event Action<iAdsService> onInterstitialLeaveApp;
        public event Action<iAdsService> onInterstitialClosed;
        public event Action<iAdsService> onRewardVideoStart;
        public event Action<iAdsService> onRewardVideoOpening;
        public event Action<iAdsService> onRewardVideoLoaded;
        public event Action<iAdsService> onRewardVideoRequested;
        public event Action<iAdsService> onRewardVideoFailed;
        public event Action<iAdsService> onRewardVideoLeaveApp;
        public event Action<iAdsService> onRewardedVideo;
        public event Action<iAdsService> onRewardFailedToShow;
        public event Action<iAdsService> onRewardVideoClosed;

        public void HideBanner()
        {
            
        }

        public void RequestBanner()
        {
           
        }

        public void RequestInterstitial()
        {
           
        }

        public void RequestRewardVideo()
        {
            
        }

        public void ShowBanner()
        {
            
        }

        public void ShowInterstitial()
        {
            
        }

        public void ShowRewardVideo()
        {
           
        }
        #endregion

        public UnityAds()
        {

        }
    }
}
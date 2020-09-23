using System;

namespace EveSoft.Ads
{
    public interface iAdsService 
    {
        #region Events
        event Action<iAdsService> onBannerLeaveApp;
        event Action<iAdsService> onBannerOpening;
        event Action<iAdsService> onBannerClosed;
        event Action<iAdsService> onBannerFailed;
        event Action<iAdsService> onBannerLoad;
        event Action<iAdsService> onInterstitialOpening;
        event Action<iAdsService> onInterstitialRequested;
        event Action<iAdsService> onInterstitialLoaded;
        event Action<iAdsService> onInterstitialFailed;
        event Action<iAdsService> onInterstitialLeaveApp;
        event Action<iAdsService> onInterstitialClosed;
        event Action<iAdsService> onRewardVideoStart;
        event Action<iAdsService> onRewardVideoOpening;
        event Action<iAdsService> onRewardVideoLoaded;
        event Action<iAdsService> onRewardVideoRequested;
        event Action<iAdsService> onRewardVideoFailed;
        event Action<iAdsService> onRewardVideoLeaveApp;
        event Action<iAdsService> onRewardedVideo;
        event Action<iAdsService> onRewardFailedToShow;
        event Action<iAdsService> onRewardVideoClosed;
        #endregion
        
        bool isBannerLoaded{get;}
        bool isRewardLoaded{get;}
        bool isInterstitialLoaded{get;}

        void ShowBanner();  
        void ShowInterstitial();
        void ShowRewardVideo();             
        void HideBanner();
        
        void RequestInterstitial();      
        void RequestRewardVideo();     
    }
}
#if ODIN_INSPECTOR 
using System;

namespace Evesoft.Ads
{
    public interface iAdsService 
    {
        #region Events
        event Action<iAdsService> onInited;
        event Action<iAdsService> onBannerRequested;
        event Action<iAdsService> onBannerLeaveApp;
        event Action<iAdsService> onBannerOpening;
        event Action<iAdsService> onBannerClosed;
        event Action<iAdsService,string> onBannerLoadFailed;
        event Action<iAdsService> onBannerLoaded;
        event Action<iAdsService> onInterstitialOpening;
        event Action<iAdsService> onInterstitialRequested;
        event Action<iAdsService> onInterstitialLoaded;
        event Action<iAdsService,string> onInterstitialLoadFailed;
        event Action<iAdsService> onInterstitialLeaveApp;
        event Action<iAdsService> onInterstitialClosed;
        event Action<iAdsService> onRewardVideoStart;
        event Action<iAdsService> onRewardVideoOpening;
        event Action<iAdsService> onRewardVideoLoaded;
        event Action<iAdsService> onRewardVideoRequested;
        event Action<iAdsService,string> onRewardVideoLoadFailed;
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
        
        void RequestBanner();
        void RequestInterstitial();      
        void RequestRewardVideo();     
    }
}
#endif
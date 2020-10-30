#if ODIN_INSPECTOR 
using System;

namespace Evesoft.Ads
{
    public interface IAdsService 
    {
        #region Events
        event Action<IAdsService> onInited;
        event Action<IAdsService> onBannerRequested;
        event Action<IAdsService> onBannerLeaveApp;
        event Action<IAdsService> onBannerOpening;
        event Action<IAdsService> onBannerClosed;
        event Action<IAdsService,string> onBannerLoadFailed;
        event Action<IAdsService> onBannerLoaded;
        event Action<IAdsService> onInterstitialOpening;
        event Action<IAdsService> onInterstitialRequested;
        event Action<IAdsService> onInterstitialLoaded;
        event Action<IAdsService,string> onInterstitialLoadFailed;
        event Action<IAdsService> onInterstitialLeaveApp;
        event Action<IAdsService> onInterstitialClosed;
        event Action<IAdsService> onRewardVideoStart;
        event Action<IAdsService> onRewardVideoOpening;
        event Action<IAdsService> onRewardVideoLoaded;
        event Action<IAdsService> onRewardVideoRequested;
        event Action<IAdsService,string> onRewardVideoLoadFailed;
        event Action<IAdsService> onRewardVideoLeaveApp;
        event Action<IAdsService> onRewardedVideo;
        event Action<IAdsService> onRewardFailedToShow;
        event Action<IAdsService> onRewardVideoClosed;
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
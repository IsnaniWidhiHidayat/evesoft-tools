#if UNITY_ADS

using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Evesoft.Ads.UnityAds
{
    internal class UnityAds : iAdsService,IUnityAdsListener
    {
        #region private
        private string _gameID,_bannerID,_interstitialID,_rewardID;
        private bool _testMode;

        private BannerLoadOptions _bannerLoadOptions;
        private BannerOptions _bannerOptions;
        #endregion

        #region iAdsService
        public bool isBannerLoaded => Advertisement.Banner.isLoaded;
        public bool isRewardLoaded => Advertisement.IsReady(_rewardID);
        public bool isInterstitialLoaded => Advertisement.IsReady(_interstitialID); 

        public event Action<iAdsService> onInited;
        public event Action<iAdsService> onBannerRequested;
        public event Action<iAdsService> onBannerLeaveApp;
        public event Action<iAdsService> onBannerOpening;
        public event Action<iAdsService> onBannerClosed;
        public event Action<iAdsService, string> onBannerLoadFailed;
        public event Action<iAdsService> onBannerLoaded;
        public event Action<iAdsService> onInterstitialOpening;
        public event Action<iAdsService> onInterstitialRequested;
        public event Action<iAdsService> onInterstitialLoaded;
        public event Action<iAdsService, string> onInterstitialLoadFailed;
        public event Action<iAdsService> onInterstitialLeaveApp;
        public event Action<iAdsService> onInterstitialClosed;
        public event Action<iAdsService> onRewardVideoStart;
        public event Action<iAdsService> onRewardVideoOpening;
        public event Action<iAdsService> onRewardVideoLoaded;
        public event Action<iAdsService> onRewardVideoRequested;
        public event Action<iAdsService, string> onRewardVideoLoadFailed;
        public event Action<iAdsService> onRewardVideoLeaveApp;
        public event Action<iAdsService> onRewardedVideo;
        public event Action<iAdsService> onRewardFailedToShow;
        public event Action<iAdsService> onRewardVideoClosed;
        
        public void RequestBanner()
        {
            if(!Advertisement.isInitialized)
                return;
            
            Advertisement.Banner.Load(_bannerID,_bannerLoadOptions);
            "{0} - {1}".LogFormat(this.GetType(),nameof(onBannerRequested));
            onBannerRequested?.Invoke(this);
        }
        public void RequestInterstitial()
        {
            if(!Advertisement.isInitialized)
                return;

            Advertisement.Load(_interstitialID);
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialRequested));
            onInterstitialRequested?.Invoke(this);
        }
        public void RequestRewardVideo()
        {
            if(!Advertisement.isInitialized)
                return;

            Advertisement.Load(_rewardID);
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardVideoRequested));
            onRewardVideoRequested?.Invoke(this);
        }
        public void ShowBanner()
        {
            if(!Advertisement.isInitialized || !isBannerLoaded)
                return;

            Advertisement.Banner.Show(_bannerID,_bannerOptions);
        }
        public void ShowInterstitial()
        {
            if(!Advertisement.isInitialized || !isInterstitialLoaded)
                return;

            Advertisement.Show(_interstitialID);
        }
        public void ShowRewardVideo()
        {
            if(!Advertisement.isInitialized || !isRewardLoaded)
                return;

            Advertisement.Show(_rewardID);
        }
        public void HideBanner()
        {
            if(!Advertisement.isInitialized)
                return;

            Advertisement.Banner.Hide();   
        }
        #endregion

        #region constructor
        public UnityAds(iAdsConfig config)
        {
            //set gameID
            switch(Application.platform)
            {
                case RuntimePlatform.Android:
                {
                    _gameID  = config.GetConfig<string>(UnityAdsConfig.GAME_ID_PLAYSTORE);
                    break;
                }

                case RuntimePlatform.IPhonePlayer:
                {
                    _gameID  = config.GetConfig<string>(UnityAdsConfig.GAME_ID_APPSTORE);
                    break;
                }

                default:
                {
                    _gameID  = config.GetConfig<string>(UnityAdsConfig.GAME_ID_PLAYSTORE);
                    break;
                }
            }

            _bannerID           = config.GetConfig<string>(UnityAdsConfig.BANNER_ID);
            _interstitialID     = config.GetConfig<string>(UnityAdsConfig.INTERSTITIAL_ID);
            _rewardID           = config.GetConfig<string>(UnityAdsConfig.REWARD_ID);
            _testMode           = config.GetConfig<bool>(UnityAdsConfig.TEST_MODE);
            
            _bannerLoadOptions = new BannerLoadOptions();
            _bannerLoadOptions.errorCallback += (error) => onBannerLoadFailed?.Invoke(this,error);
            _bannerLoadOptions.loadCallback  += ()=> onBannerLoaded?.Invoke(this);
            
            _bannerOptions = new BannerOptions();
            _bannerOptions.showCallback += ()=> onBannerOpening?.Invoke(this);
            _bannerOptions.hideCallback += ()=> onBannerClosed?.Invoke(this);
 
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameID,_testMode);

            if(Advertisement.isInitialized)
            {
                onInited?.Invoke(this);
            }    
            else
            {
               WaitUntilInited();
            }
        }
        #endregion

        private async void WaitUntilInited()
        {
            await new WaitUntil(()=> Advertisement.isInitialized);
            onInited?.Invoke(this);
        }

        #region IUnityAdsListener
        public void OnUnityAdsReady(string placementId)
        {
           if(placementId == _bannerID)
           {
               onBannerLoaded?.Invoke(this);
           }
           else if(placementId == _interstitialID)
           {
               onInterstitialLoaded?.Invoke(this);
           }
           else if(placementId == _rewardID)
           {
               onRewardVideoLoaded?.Invoke(this);
           }
        }
        public void OnUnityAdsDidError(string message)
        {
            
        }
        public void OnUnityAdsDidStart(string placementId)
        {
            if(placementId == _bannerID)
            {
                onBannerOpening?.Invoke(this);
            }
            else if(placementId == _interstitialID)
            {
                onInterstitialOpening?.Invoke(this);
            }
            else if(placementId == _rewardID)
            {
                onRewardVideoOpening?.Invoke(this);
                onRewardVideoStart?.Invoke(this);
            }
        }
        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            if(placementId == _bannerID)
            {
                onBannerClosed?.Invoke(this);
            }
            else if(placementId == _interstitialID)
            {
                onInterstitialClosed?.Invoke(this);
            }
            else if(placementId == _rewardID)
            {
                switch(showResult)
                {
                    case ShowResult.Failed:
                    {
                        onRewardFailedToShow?.Invoke(this);
                        break;
                    }

                    case ShowResult.Skipped:{
                        onRewardVideoClosed?.Invoke(this);
                        break;
                    }

                    case ShowResult.Finished:{
                        onRewardedVideo.Invoke(this);
                        break;
                    }
                }
            }
        }
        #endregion
    }
}

#endif
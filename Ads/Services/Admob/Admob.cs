using System;
using Sirenix.OdinInspector;
using GoogleMobileAds.Api;
using UnityEngine;

namespace Evesoft.Ads.Admob
{
    [HideMonoScript]
    public class Admob : iAdsService,IDisposable
    {
        #region private
        private string _bannerID,_interstitialID,_rewardID;
        private bool _tagForChild;
        private AdsGender _gender;
        private AdPosition _bannerPosition;
        private Vector2Int _customPosition;
        private string[] _keywords;

        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardVideoAd;
        private bool _isBannerLoaded;
        private bool _inited;
        #endregion

        #region Constructor
        public Admob(iAdsConfig config)
        {
            _bannerID = config.GetConfig<string>(AdmobConfig.BANNER_ID);
            _interstitialID = config.GetConfig<string>(AdmobConfig.INTERSTITIAL_ID);
            _rewardID = config.GetConfig<string>(AdmobConfig.REWARD_ID);
            _tagForChild = config.GetConfig<bool>(AdmobConfig.TAG_FOR_CHILD);
            _gender = config.GetConfig<AdsGender>(AdmobConfig.GENDER);
            _bannerPosition = config.GetConfig<AdPosition>(AdmobConfig.BANNER_POSITION);
            _customPosition = config.GetConfig<Vector2Int>(AdmobConfig.CUSTOM_POSITION);
            _keywords = config.GetConfig<string[]>(AdmobConfig.KEY_WORDS);
            MobileAds.Initialize(OnInited);   
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if(!_bannerView.IsNull())
            {
                _bannerView.OnAdLoaded -= OnBannerLoad;
                _bannerView.OnAdFailedToLoad -= OnBannerFailed;
                _bannerView.OnAdClosed -= OnBannerClosed;
                _bannerView.OnAdOpening -= OnBannerOpening;
                _bannerView.OnAdLeavingApplication -= OnBannerLeaveApp;
            }

            if(!_interstitialAd.IsNull())
            {
                _interstitialAd.OnAdClosed -= OnInterstitialClosed;
                _interstitialAd.OnAdFailedToLoad -= OnInterstitialFailed;
                _interstitialAd.OnAdLeavingApplication -= OnInterstitialLeaveApp;
                _interstitialAd.OnAdLoaded -= OnInterstitialLoaded;
                _interstitialAd.OnAdOpening -= OnInterstitialOpening;
            }

            if(!_rewardVideoAd.IsNull())
            {
                _rewardVideoAd.OnAdLoaded -= OnRewardLoaded;
                _rewardVideoAd.OnAdFailedToLoad -= OnRewardFailedLoad;
                _rewardVideoAd.OnAdOpening -= OnRewardOpening;
                _rewardVideoAd.OnAdFailedToShow -= OnRewardFailedToShow;
                _rewardVideoAd.OnUserEarnedReward -= OnRewarded;
                _rewardVideoAd.OnAdClosed -= OnRewardClosed;
            }
        }     
        #endregion
        
        #region iAdsService
        public event Action<iAdsService> onBannerLeaveApp, 
        onBannerOpening,onBannerClosed,onBannerFailed,onBannerLoad, 
        onInterstitialOpening,onInterstitialRequested,onInterstitialLoaded, 
        onInterstitialLeaveApp, onInterstitialFailed,onInterstitialClosed, 
        onRewardVideoStart,onRewardVideoOpening,onRewardVideoRequested,
        onRewardVideoLoaded,onRewardVideoLeaveApp,onRewardVideoFailed, 
        onRewardedVideo,onRewardFailedToShow,onRewardVideoClosed;

        public bool isBannerLoaded => _isBannerLoaded;
        public bool isRewardLoaded => _rewardVideoAd.IsNull()? false : _rewardVideoAd.IsLoaded();
        public bool isInterstitialLoaded => _interstitialAd.IsNull() ? false : _interstitialAd.IsLoaded();

        public void RequestBanner()
        {
            if(!_inited)
                return;

            if(_bannerID.IsNullOrEmpty())
                return;

            if(_bannerView.IsNull())
            {
                _bannerView                         = _bannerPosition == AdPosition.Custom? new BannerView(_bannerID,AdSize.SmartBanner,_customPosition.x,_customPosition.y) : new BannerView(_bannerID,AdSize.SmartBanner,(GoogleMobileAds.Api.AdPosition)((int)_bannerPosition));
                _bannerView.OnAdLoaded              += OnBannerLoad;
                _bannerView.OnAdFailedToLoad        += OnBannerFailed;
                _bannerView.OnAdClosed              += OnBannerClosed;
                _bannerView.OnAdOpening             += OnBannerOpening;
                _bannerView.OnAdLeavingApplication  += OnBannerLeaveApp;
            }
        }
        public void RequestInterstitial()
        {
            if(!_inited)
                return;

            if (_interstitialID.IsNullOrEmpty())
                return;

            if(!_interstitialAd.IsNull())
            {
                _interstitialAd.OnAdClosed -= OnInterstitialClosed;
                _interstitialAd.OnAdFailedToLoad -= OnInterstitialFailed;
                _interstitialAd.OnAdLeavingApplication -= OnInterstitialLeaveApp;
                _interstitialAd.OnAdLoaded -= OnInterstitialLoaded;
                _interstitialAd.OnAdOpening -= OnInterstitialOpening;
            }

            if(!isInterstitialLoaded)
            {   
                var request = RequestAd(_gender, _tagForChild, _keywords);    
                _interstitialAd = new InterstitialAd(_interstitialID);
                _interstitialAd.OnAdClosed += OnInterstitialClosed;
                _interstitialAd.OnAdFailedToLoad += OnInterstitialFailed;
                _interstitialAd.OnAdLeavingApplication += OnInterstitialLeaveApp;
                _interstitialAd.OnAdLoaded += OnInterstitialLoaded;
                _interstitialAd.OnAdOpening += OnInterstitialOpening; 

                _interstitialAd.LoadAd(request);
                OnInterstitialRequested();
            }
            else
            {
                OnInterstitialLoaded(_interstitialAd,null);
            }     
        }
        public void RequestRewardVideo()
        {
            if(!_inited)
                return;

            if (_rewardID.IsNullOrEmpty())
                return;

            if (!_rewardVideoAd.IsNull())
            {
                _rewardVideoAd.OnAdLoaded -= OnRewardLoaded;
                _rewardVideoAd.OnAdFailedToLoad -= OnRewardFailedLoad;
                _rewardVideoAd.OnAdOpening -= OnRewardOpening;
                _rewardVideoAd.OnAdFailedToShow -= OnRewardFailedToShow;
                _rewardVideoAd.OnUserEarnedReward -= OnRewarded;
                _rewardVideoAd.OnAdClosed -= OnRewardClosed;
            }

            if (!isRewardLoaded)
            {
                var request = RequestAd(_gender, _tagForChild, _keywords);
                _rewardVideoAd = new RewardedAd(_rewardID);
                _rewardVideoAd.OnAdLoaded += OnRewardLoaded;
                _rewardVideoAd.OnAdFailedToLoad += OnRewardFailedLoad;
                _rewardVideoAd.OnAdOpening += OnRewardOpening;
                _rewardVideoAd.OnAdFailedToShow += OnRewardFailedToShow;
                _rewardVideoAd.OnUserEarnedReward += OnRewarded;
                _rewardVideoAd.OnAdClosed += OnRewardClosed;

                _rewardVideoAd.LoadAd(request);
                OnRewardRequested();
            }
            else
            {
                OnRewardLoaded(_rewardVideoAd, null);
            }
        }
        
        public void ShowBanner()
        {   
            if(!_inited)
                return;

            if(_bannerView.IsNull())
                return;

            if(_isBannerLoaded) 
                _bannerView.Show();
            else
                _bannerView.LoadAd(RequestAd(_gender, _tagForChild, _keywords));
        }
        public void HideBanner()
        {
            if(!_inited)
                return;

            if (_bannerView.IsNull())
                return;

            _bannerView.Hide();
        }    
        public void ShowInterstitial()
        {
            if(!_inited)
                return;
            
            if (_interstitialAd == null)
                return;

            if (_interstitialAd.IsLoaded())
                _interstitialAd.Show();
        }     
        public void ShowRewardVideo()
        {
            if(!_inited)
                return;
                
            if (_rewardVideoAd == null)
                return;

            if (_rewardVideoAd.IsLoaded())
                _rewardVideoAd.Show();
        }
        #endregion
        
        #region methods
        private AdRequest RequestAd(AdsGender gender, bool tagForChildren, params string[] keywords)
        {
            var adReq = new AdRequest.Builder();

            //Set Gender
            switch(gender)
            {
                case AdsGender.Male:
                {
                    adReq.SetGender(GoogleMobileAds.Api.Gender.Male);
                    break;
                }

                case AdsGender.Female:
                {
                    adReq.SetGender(GoogleMobileAds.Api.Gender.Female);
                    break;
                }

                default:
                {
                    adReq.SetGender(GoogleMobileAds.Api.Gender.Unknown);
                    break;
                }
            }

            //Set Tag for child
            if(tagForChildren)
                adReq.TagForChildDirectedTreatment(tagForChildren);

            //Set KeyWord
            if(!keywords.IsNullOrEmpty())
            {
                 for (int i = 0; i < keywords.Length; i++)
                {
                    if (string.IsNullOrEmpty(keywords[i]))
                        continue;

                    adReq.AddKeyword(keywords[i]);
                }
            }

            return adReq.Build();
        }   
        #endregion

        #region CallBack
        private void OnInited(InitializationStatus status)
        {
            _inited = true;
            RequestBanner();
        }
        
        private void OnBannerLeaveApp(object sender, EventArgs e)
        {
            _isBannerLoaded = false;

            "{0} - {1}".LogFormat(this.GetType(),nameof(onBannerLeaveApp));
            onBannerLeaveApp?.Invoke(this);
        }
        private void OnBannerOpening(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onBannerOpening));
            onBannerOpening?.Invoke(this);
        }
        private void OnBannerClosed(object sender, EventArgs e)
        {
            _isBannerLoaded = false;
            "{0} - {1}".LogFormat(this.GetType(),nameof(onBannerClosed));     
            onBannerClosed?.Invoke(this);
        }
        private void OnBannerFailed(object sender, AdFailedToLoadEventArgs e)
        {
            _isBannerLoaded = false;
            "{0} - {1}".LogErrorFormat(this.GetType(),nameof(onBannerFailed));
            onBannerFailed?.Invoke(this);
        }
        private void OnBannerLoad(object sender, EventArgs e)
        {
            _isBannerLoaded = true;
            "{0} - {1}".LogFormat(this.GetType(),nameof(onBannerLoad));
            onBannerLoad?.Invoke(this);
        }

        private void OnInterstitialOpening(object sender, EventArgs e)
        {   
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialOpening));
            onInterstitialOpening?.Invoke(this);
        }
        private void OnInterstitialRequested()
        {
           "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialRequested));
            onInterstitialRequested?.Invoke(this);
        }
        private void OnInterstitialLoaded(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialLoaded));
            onInterstitialLoaded?.Invoke(this);  
        }
        private void OnInterstitialLeaveApp(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialLeaveApp));
            onInterstitialLeaveApp?.Invoke(this);  
        }
        private void OnInterstitialFailed(object sender, AdFailedToLoadEventArgs e)
        {
            "{0} - {1}".LogErrorFormat(this.GetType(),nameof(onInterstitialFailed));
            onInterstitialFailed?.Invoke(this);   
        }
        private void OnInterstitialClosed(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onInterstitialClosed));
            onInterstitialClosed?.Invoke(this);   
        }

        private void OnRewardOpening(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardVideoOpening));
            onRewardVideoOpening?.Invoke(this);   
        }
        private void OnRewardRequested()
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardVideoRequested));
            onRewardVideoRequested?.Invoke(this);
        }
        private void OnRewardLoaded(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardVideoLoaded));
            onRewardVideoLoaded?.Invoke(this);   
        }
        private void OnRewardFailedLoad(object sender, AdErrorEventArgs e)
        {
            "{0} - {1} - {2}".LogErrorFormat(this.GetType(),nameof(onRewardVideoFailed),e.Message);
            onRewardVideoFailed?.Invoke(this);   
        }
        private void OnRewarded(object sender, Reward e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardedVideo));
            onRewardedVideo?.Invoke(this); 
        }
        private void OnRewardFailedToShow(object sender, EventArgs e)
        {
            "{0} - {1}".LogErrorFormat(this.GetType(),nameof(onRewardFailedToShow));
            onRewardFailedToShow?.Invoke(this);    
        }
        private void OnRewardClosed(object sender, EventArgs e)
        {
            "{0} - {1}".LogFormat(this.GetType(),nameof(onRewardVideoClosed));
            onRewardVideoClosed?.Invoke(this);
        }
        #endregion
    }
} 
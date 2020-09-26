#if UNITY_ADS

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.Ads.UnityAds
{

    [Serializable,HideReferenceObjectPicker]
    internal class UnityAdsConfig : iAdsConfig
    {
        #region const
        public const string GAME_ID_PLAYSTORE = nameof(GAME_ID_PLAYSTORE);
        public const string GAME_ID_APPSTORE = nameof(GAME_ID_APPSTORE);
        public const string BANNER_ID = nameof(BANNER_ID);
        public const string INTERSTITIAL_ID = nameof(INTERSTITIAL_ID);
        public const string REWARD_ID = nameof(REWARD_ID);

        public const string TEST_MODE = nameof(TEST_MODE);

        const string grpMain = "Main";
        const string grpBanner = "Banner";
        const string grpInstantitial = "Instantitial";
        const string grpRewardVideo = "Reward Video";
        #endregion

        #region fields
       
        [FoldoutGroup(grpMain)]
        public string playStoreID;

        [FoldoutGroup(grpMain)]
        public string appleStoreID;

        
        [FoldoutGroup(grpMain)]
        public bool testMode;

        [FoldoutGroup(grpBanner),LabelText("ID")]
        public string bannerID;

        [FoldoutGroup(grpInstantitial),LabelText("ID")]
        public string interstitialID;

        [FoldoutGroup(grpRewardVideo),LabelText("ID")]
        public string rewardID; 
        #endregion

        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iAdsConfig
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }      
        #endregion
    
        #region constructor
        public UnityAdsConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(Ads)] = AdsType.UnityAds;
        }
        public UnityAdsConfig(string playstoreID,string appleStoreID,string bannerID,string interstitialID,string rewardID,bool testMode):this()
        {
            _configs[GAME_ID_PLAYSTORE] = this.playStoreID    = playstoreID;
            _configs[GAME_ID_APPSTORE]  = this.appleStoreID   = appleStoreID;
            _configs[BANNER_ID]         = this.bannerID           = bannerID;
            _configs[INTERSTITIAL_ID]   = this.interstitialID     = interstitialID;
            _configs[REWARD_ID]         = this.rewardID           = rewardID;
            _configs[TEST_MODE]         = this.testMode           = testMode;
        }
        #endregion
    }
}
#endif
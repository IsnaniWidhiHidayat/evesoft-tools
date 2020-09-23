using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.Ads.UnityAds
{

    [Serializable,HideReferenceObjectPicker]
    public class UnityAdsConfig : iAdsConfig
    {
        #region const
        public const string GAME_ID = nameof(GAME_ID);
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
        [FoldoutGroup(grpMain),HorizontalGroup(grpMain+"/h1",width:120),HideLabel]
        public UnityAdsStore store;

        [ShowIf(nameof(store),UnityAdsStore.PlayStore),FoldoutGroup(grpMain),HideLabel,SuffixLabel("Game ID",true),HorizontalGroup(grpMain+"/h1"),VerticalGroup(grpMain+"/h1/v1")]
        public string playStoreGameID;

        [ShowIf(nameof(store),UnityAdsStore.AppleStore),FoldoutGroup(grpMain),HideLabel,SuffixLabel("Game ID",true),VerticalGroup(grpMain+"/h1/v1")]
        public string appStoreGameID;

        
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
        public IDictionary<string, object> configs
        {
            get
            {
                if(_configs.IsNull())
                {
                    _configs = new Dictionary<string,object>();
                    _configs[GAME_ID]           = store == UnityAdsStore.PlayStore? playStoreGameID : appStoreGameID;
                    _configs[BANNER_ID]         = bannerID;
                    _configs[INTERSTITIAL_ID]   = interstitialID;
                    _configs[REWARD_ID]         = rewardID;
                    _configs[TEST_MODE]         = testMode;
                }

                return _configs;
            }
        }
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }      
        #endregion
    
        #region const
        public UnityAdsConfig(){}
        public UnityAdsConfig(UnityAdsStore store,string playstoreID,string appleStoreID,string bannerID,string interstitialID,string rewardID,bool testMode)
        {
            this.store = store;
            this.bannerID = bannerID;
            this.interstitialID = interstitialID;
            this.rewardID = rewardID;
            this.playStoreGameID = playstoreID;
            this.appStoreGameID = appleStoreID;
            this.testMode = testMode;
        }
        #endregion
    }
}
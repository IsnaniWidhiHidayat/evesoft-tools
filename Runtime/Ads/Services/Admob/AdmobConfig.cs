#if ODIN_INSPECTOR 
#if ADMOB
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Ads.Admob
{   
    [Serializable,HideReferenceObjectPicker]
    internal class AdmobConfig : iAdsConfig
    {
        #region const
        public const string BANNER_ID = nameof(BANNER_ID);
        public const string INTERSTITIAL_ID = nameof(INTERSTITIAL_ID);
        public const string REWARD_ID = nameof(REWARD_ID);
        public const string TAG_FOR_CHILD = nameof(TAG_FOR_CHILD);
        public const string GENDER = nameof(GENDER);
        public const string BANNER_POSITION = nameof(BANNER_POSITION);
        public const string CUSTOM_POSITION = nameof(CUSTOM_POSITION);
        public const string KEY_WORDS = nameof(KEY_WORDS);

        const string grpBanner = "Banner";
        const string grpInstantitial = "Instantitial";
        const string grpRewardVideo = "Reward Video";
        const string grpRequest   = "Request";
        #endregion

        #region field
        [FoldoutGroup(grpBanner),LabelText("ID")]
        public string bannerID;

        [FoldoutGroup(grpInstantitial),LabelText("ID")]
        public string interstitialID;

        [FoldoutGroup(grpRewardVideo),LabelText("ID")]
        public string rewardID;
        
        [FoldoutGroup(grpRequest)]
        public bool tagForChild;

        [FoldoutGroup(grpRequest)]
        public AdsGender gender;

        [FoldoutGroup(grpBanner),LabelText("Position")]
        public AdPosition bannerPosition;

        [SerializeField,ShowIf(nameof(bannerPosition),AdPosition.Custom),FoldoutGroup(grpBanner)] 
        public Vector2Int customPosition;

        [FoldoutGroup(grpRequest)]
        public string[] keywords = new string[0];
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
        public AdmobConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(Ads)] = AdsType.Admob;
        }
        public AdmobConfig(string bannerID,string interstitialID,string rewardID,bool tagForChild,AdsGender gender,AdPosition bannerPosition,Vector2Int customPosition,string[] keywords):this()
        {
            _configs[BANNER_ID]         = this.bannerID          = bannerID;
            _configs[INTERSTITIAL_ID]   = this.interstitialID    = interstitialID;
            _configs[REWARD_ID]         = this.rewardID          = rewardID;
            _configs[TAG_FOR_CHILD]     = this.tagForChild       = tagForChild;
            _configs[GENDER]            = this.gender            = gender;
            _configs[BANNER_POSITION]   = this.bannerPosition    = bannerPosition;
            _configs[CUSTOM_POSITION]   = this.customPosition    = customPosition;
            _configs[KEY_WORDS]         = this.keywords          = keywords;
        }
        #endregion
    }
}
#endif
#endif
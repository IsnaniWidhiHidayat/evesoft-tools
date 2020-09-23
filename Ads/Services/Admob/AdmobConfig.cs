using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Ads.Admob
{   
    [Serializable,HideReferenceObjectPicker]
    public class AdmobConfig : iAdsConfig
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
        public IDictionary<string, object> configs
        {
            get
            {
                if(_configs.IsNull())
                {
                    _configs = new Dictionary<string,object>();
                    _configs[BANNER_ID]         = bannerID;
                    _configs[INTERSTITIAL_ID]   = interstitialID;
                    _configs[REWARD_ID]         = rewardID;
                    _configs[TAG_FOR_CHILD]     = tagForChild;
                    _configs[GENDER]            = gender;
                    _configs[BANNER_POSITION]   = bannerPosition;
                    _configs[CUSTOM_POSITION]   = customPosition;
                    _configs[KEY_WORDS]         = keywords;
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

        #region constructor
        internal AdmobConfig(){}
        internal AdmobConfig(string bannerID,string interstitialID,string rewardID,bool tagForChild,AdPosition bannerPosition,Vector2Int customPosition,string[] keywords)
        {
            this.bannerID          = bannerID;
            this.interstitialID    = interstitialID;
            this.rewardID          = rewardID;
            this.tagForChild       = tagForChild;
            this.bannerPosition    = bannerPosition;
            this.customPosition    = customPosition;
            this.keywords          = keywords;
        }
        #endregion
    }
}
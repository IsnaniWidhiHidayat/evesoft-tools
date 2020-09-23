using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Ads.Admob
{   
    [HideMonoScript]
    [Serializable]
    [CreateAssetMenu(menuName = "Evesoft/Ads/Admob/Config")]
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
        #endregion

        #region field
        [SerializeField] 
        private string _bannerID,_interstitialID,_rewardID;

        [SerializeField] 
        private bool _tagForChild;

        [SerializeField] 
        private AdsGender _gender;

        [SerializeField] 
        private AdPosition _bannerPosition;

        [SerializeField,ShowIf(nameof(_bannerPosition),AdPosition.Custom)] 
        private Vector2Int _customPosition;
        
        [SerializeField] 
        private string[] _keywords;
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
                    _configs[BANNER_ID]         = _bannerID;
                    _configs[INTERSTITIAL_ID]   = _interstitialID;
                    _configs[REWARD_ID]         = _rewardID;
                    _configs[TAG_FOR_CHILD]     = _tagForChild;
                    _configs[GENDER]            = _gender;
                    _configs[BANNER_POSITION]   = _bannerPosition;
                    _configs[CUSTOM_POSITION]   = _customPosition;
                    _configs[KEY_WORDS]         = _keywords;
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
            this._bannerID          = bannerID;
            this._interstitialID    = interstitialID;
            this._rewardID          = rewardID;
            this._tagForChild       = tagForChild;
            this._bannerPosition    = bannerPosition;
            this._customPosition    = customPosition;
            this._keywords          = keywords;
        }
        #endregion
    }
}
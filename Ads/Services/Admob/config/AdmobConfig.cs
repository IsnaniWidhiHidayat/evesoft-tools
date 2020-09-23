using Sirenix.OdinInspector;
using UnityEngine;

namespace EveSoft.Ads.Admob
{   
    [HideMonoScript]
    [CreateAssetMenu(menuName = "Evesoft/Ads/Admob/Config")]
    public class AdmobConfig : SerializedScriptableObject ,iAdsConfig
    {
        #region field
        [SerializeField] private string _appId;
        [SerializeField] private string _bannerID;
        [SerializeField] private string _interstitialID ;
        [SerializeField] private string _rewardID;
        [SerializeField] private bool _tagForChild;
        [SerializeField] private AdsGender _gender;
        [SerializeField] private AdPosition _bannerPosition;
        [SerializeField,ShowIf("_bannerPosition",AdPosition.Custom)] private Vector2Int _customPosition;
        [SerializeField] private string[] _keywords;
        #endregion

        #region iAdsConfig
        public string appId
        {
            get => _appId;
            set => appId = value;
        }
        public string bannerID{
            get => _bannerID;
            set => _bannerID = value;
        }
        public string interstitialID{
            get => _interstitialID;
            set => _interstitialID = value;
        }
        public string rewardID{
            get => _rewardID;
            set => _rewardID = value;
        }
        public bool tagForChild{
            get => _tagForChild;
            set => _tagForChild = value;
        }
        public AdsGender gender{
            get=> _gender;
            set => _gender = value;
        }
        public AdPosition bannerPosition{
            get => _bannerPosition;
            set => _bannerPosition = value;
        }
        public Vector2Int customPosition{
            get => _customPosition;
            set => _customPosition  = value;
        }
        public string[] keywords {
            get => _keywords;
            set => _keywords = value;
        }
        #endregion
    }
}
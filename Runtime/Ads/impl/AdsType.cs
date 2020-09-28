#if ODIN_INSPECTOR 
namespace Evesoft.Ads
{
    public enum AdsType
    {
        None,
#if ADMOB
        Admob,
#endif
#if UNITY_ADS
        UnityAds
#endif
    }
}
#endif
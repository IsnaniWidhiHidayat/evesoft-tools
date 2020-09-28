#if ODIN_INSPECTOR 
namespace Evesoft.Ads
{
    public interface iAdsConfig
    {
        T GetConfig<T>(string key);
    }
}


#endif
#if ODIN_INSPECTOR 
namespace Evesoft.Ads
{
    public interface IAdsConfig
    {
        T GetConfig<T>(string key);
    }
}


#endif
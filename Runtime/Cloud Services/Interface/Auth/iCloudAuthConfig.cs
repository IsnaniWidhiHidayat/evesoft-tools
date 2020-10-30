#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public interface ICloudAuthConfig
    {
        T GetConfig<T>(string key);
    }
}



#endif
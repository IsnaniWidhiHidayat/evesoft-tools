#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public interface ICloudDatabaseConfig
    {
        T GetConfig<T>(string key);
    }
}



#endif
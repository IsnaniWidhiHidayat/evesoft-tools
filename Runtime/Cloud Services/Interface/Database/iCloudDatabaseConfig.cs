#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public interface iCloudDatabaseConfig
    {
        T GetConfig<T>(string key);
    }
}



#endif
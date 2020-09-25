namespace Evesoft.CloudService
{
    public interface iCloudDatabaseConfig
    {
        T GetConfig<T>(string key);
    }
}



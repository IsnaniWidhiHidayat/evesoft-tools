namespace Evesoft.CloudService
{
    public interface iCloudAuthConfig
    {
        T GetConfig<T>(string key);
    }
}



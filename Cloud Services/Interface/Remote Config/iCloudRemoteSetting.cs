
namespace Evesoft.CloudService
{
    public interface iCloudRemoteSetting
    {
        T GetConfig<T>(string key);
    }
}



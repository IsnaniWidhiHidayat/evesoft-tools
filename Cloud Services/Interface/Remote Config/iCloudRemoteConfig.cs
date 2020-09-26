using System.Threading.Tasks;

namespace Evesoft.CloudService
{
    public interface iCloudRemoteConfig
    {
        bool isHaveConfigs{get;}
        bool isfetched{get;}
        T GetConfig<T>(string key);
        Task Fetch();
    }
}


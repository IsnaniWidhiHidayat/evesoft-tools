#if ODIN_INSPECTOR 
using System.Threading.Tasks;

namespace Evesoft.CloudService
{
    public interface ICloudRemoteConfig
    {
        bool isHaveConfigs{get;}
        bool isfetched{get;}
        T GetConfig<T>(string key);
        Task Fetch();
    }
}


#endif
#if ODIN_INSPECTOR 

namespace Evesoft.CloudService
{
    public interface iCloudRemoteSetting
    {
        T GetConfig<T>(string key);
    }
}



#endif
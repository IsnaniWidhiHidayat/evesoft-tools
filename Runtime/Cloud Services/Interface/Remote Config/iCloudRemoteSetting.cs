#if ODIN_INSPECTOR 

namespace Evesoft.CloudService
{
    public interface ICloudRemoteSetting
    {
        T GetConfig<T>(string key);
    }
}



#endif
#if ODIN_INSPECTOR 

namespace Evesoft.CloudService
{
    public interface ICloudAuthOptions
    {
        T GetOptions<T>(string key);
    }
}



#endif
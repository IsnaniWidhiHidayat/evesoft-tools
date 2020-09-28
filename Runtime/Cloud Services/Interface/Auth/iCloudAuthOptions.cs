#if ODIN_INSPECTOR 

namespace Evesoft.CloudService
{
    public interface iCloudAuthOptions
    {
        T GetOptions<T>(string key);
    }
}



#endif
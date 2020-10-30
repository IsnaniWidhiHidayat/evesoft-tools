#if ODIN_INSPECTOR 

using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface ICloudStorageConfig
    {
        T GetConfig<T>(string key);
    }
}



#endif
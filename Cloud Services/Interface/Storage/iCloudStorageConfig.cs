
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudStorageConfig
    {
        T GetConfig<T>(string key);
    }
}



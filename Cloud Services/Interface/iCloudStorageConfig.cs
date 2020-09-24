
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudStorageConfig
    {
        IDictionary<string,object> configs{get;}
        T GetConfig<T>(string key);
    }
}



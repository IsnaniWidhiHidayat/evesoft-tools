
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudDatabaseConfig
    {
        IDictionary<string,object> configs{get;}
        T GetConfig<T>(string key);
    }
}



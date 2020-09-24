
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudAuthConfig
    {
        IDictionary<string,object> configs{get;}
        T GetConfig<T>(string key);
    }
}



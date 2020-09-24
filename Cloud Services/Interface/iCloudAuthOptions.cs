
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudAuthOptions
    {
        T GetOptions<T>(string key);
        void SetOptions(string key,object value);
    }
}



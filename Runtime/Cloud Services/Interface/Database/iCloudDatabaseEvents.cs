#if ODIN_INSPECTOR 
using System;

namespace Evesoft.CloudService
{
    public interface ICloudDatabaseEvents
    {
        event Action<string,object> onDataAdded;
        event Action<string,object> onDataChange;
        event Action<string,object> onDataRemoved;
    }
}
#endif
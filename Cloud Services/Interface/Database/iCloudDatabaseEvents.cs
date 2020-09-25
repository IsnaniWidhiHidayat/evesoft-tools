using System;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudDatabaseEvents
    {
        event Action<string,object> onDataAdded;
        event Action<string,object> onDataChange;
        event Action<string,object> onDataRemoved;
    }
}
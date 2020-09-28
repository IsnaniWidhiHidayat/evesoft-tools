#if ODIN_INSPECTOR 
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudDatabase
    {
        Task<(iCloudDatabaseReference,Exception)> Connect(IDictionary<string,object> parameter);
        Task<Exception> Disconnect(iCloudDatabaseReference reference);
    }
}
#endif
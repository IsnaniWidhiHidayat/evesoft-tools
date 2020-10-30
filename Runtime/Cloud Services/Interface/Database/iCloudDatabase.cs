#if ODIN_INSPECTOR 
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface ICloudDatabase
    {
        Task<(ICloudDatabaseReference,Exception)> Connect(IDictionary<string,object> parameter);
        Task<Exception> Disconnect(ICloudDatabaseReference reference);
    }
}
#endif
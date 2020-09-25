using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudDatabase
    {
        Task<(iCloudDatabaseReference,Exception)> Connect(IDictionary<string,object> parameter);
        void Disconnect(iCloudDatabaseReference reference);
    }
}
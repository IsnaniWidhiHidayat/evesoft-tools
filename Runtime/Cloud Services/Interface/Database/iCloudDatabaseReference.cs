#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evesoft.CloudService
{
    public interface iCloudDatabaseReference
    {       
        iCloudDatabaseEvents events{get;}
        IDictionary<string,object> data{get;}

        Task<Exception> SetData(IDictionary<string,object> value);
        Task<Exception> RemoveData(IDictionary<string,object> value);
        Task<Exception> UpdateData(IDictionary<string,object> value);
    }
}
#endif
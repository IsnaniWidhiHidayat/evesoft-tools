using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public interface iCloudDatabase
    {
        bool inited{get;}
        Task<(iCloudDatabaseData,Exception)> GetData(string path);
        Task<Exception> SetData<T>(T data,string path,IDictionary<string,object> options = null);
        Task<Exception> RemoveData(string path);
        Task<Exception> UpdateData(IDictionary<string,object> data,string path);
        Task<(iCloudDatabaseReference,Exception)> Reference(string parameter);
    }
}
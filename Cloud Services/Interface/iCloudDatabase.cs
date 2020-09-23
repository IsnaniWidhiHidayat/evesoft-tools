using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RollingGlory.FaceApp
{
    public interface iCloudDatabase
    {
        bool inited{get;}
        Task<(iCloudDatabaseData,Exception)> GetData(string path);
        Task<Exception> SetData(object data,string path,DatabaseDataType type);
        Task<Exception> RemoveData(string path);
        Task<Exception> UpdateData(IDictionary<string,object> data,string path);
        Task<(iCloudDatabaseReference,Exception)> Reference(string parameter);
    }
}
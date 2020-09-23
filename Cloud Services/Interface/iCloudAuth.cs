using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Evesoft.CloudService
{
    public interface iCloudAuth
    {
        bool inited{get;}
        iUserAuth currentUser{get;}
        Task<(iUserAuth,Exception)> Login(IDictionary<string,object> options);
        void Logout();
    }
}
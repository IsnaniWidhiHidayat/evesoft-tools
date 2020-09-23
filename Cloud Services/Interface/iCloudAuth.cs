using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace RollingGlory.FaceApp
{
    public interface iCloudAuth
    {
        bool inited{get;}
        iUser currentUser{get;}
        Task<(iUser,Exception)> Login(IDictionary<string,object> options);
        void Logout();
    }
}
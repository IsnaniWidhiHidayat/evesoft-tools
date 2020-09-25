using System.Threading.Tasks;
using System;

namespace Evesoft.CloudService
{
    public interface iCloudAuth
    {
        bool inited{get;}
        iUserAuth currentUser{get;}
        Task<(iUserAuth,Exception)> Login(iCloudAuthOptions options);
        void Logout();
    }
}
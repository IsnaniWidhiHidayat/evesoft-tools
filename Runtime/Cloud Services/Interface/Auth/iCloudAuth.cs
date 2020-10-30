#if ODIN_INSPECTOR 
using System.Threading.Tasks;
using System;

namespace Evesoft.CloudService
{
    public interface ICloudAuth
    {
        bool inited{get;}
        IUserAuth currentUser{get;}
        Task<(IUserAuth,Exception)> Login(ICloudAuthOptions options = null);
        void Logout();
    }
}
#endif
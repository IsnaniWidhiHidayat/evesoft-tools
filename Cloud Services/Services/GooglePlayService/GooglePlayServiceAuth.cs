using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evesoft.CloudService.GooglePlayService
{
    //TODO:Implament play service
    internal class GooglePlayServiceAuth : iCloudAuth
    {
        public bool inited => throw new NotImplementedException();

        public iUserAuth currentUser => throw new NotImplementedException();

        public Task<(iUserAuth, Exception)> Login(iCloudAuthOptions options = null)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public GooglePlayServiceAuth(iCloudAuthConfig config)
        {

        }
    }
}
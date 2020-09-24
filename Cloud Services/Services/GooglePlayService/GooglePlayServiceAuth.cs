using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evesoft.CloudService.GooglePlayService
{
    //TODO:Implament play service
    public class GooglePlayServiceAuth : iCloudAuth
    {
        public bool inited => throw new NotImplementedException();

        public iUserAuth currentUser => throw new NotImplementedException();

        public Task<(iUserAuth, Exception)> Login(IDictionary<string, object> options)
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
#if ODIN_INSPECTOR 
#if GOOGLE_AUTH
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.CloudService.GoogleSignIn
{
    internal class GoogleAuth : iCloudAuth
    {
        #region private
        private iUserAuth _currentUser;
        private bool _inited;
        #endregion

        #region iCloudAuth
        public bool inited => _inited;
        public iUserAuth currentUser => _currentUser;
        
        public async Task<(iUserAuth,Exception)> Login(iCloudAuthOptions options = null)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(!_currentUser.IsNull())
                    return (_currentUser,null);
    
                var googleUser = await Google.GoogleSignIn.DefaultInstance.SignIn();
                _currentUser = new CloudAuthUser()
                {
                    authType = CloudAuthType.GoogleSignIn,
                    id       = googleUser.UserId,
                    imageUrl = googleUser.ImageUrl.ToString(),
                    name     = googleUser.DisplayName,
                    token    = googleUser.IdToken,
                    email    = googleUser.Email
                };
                return (_currentUser,null);
            } 
            catch (Exception ex) 
            {
                return (null,ex);
            }
        }
        public async void Logout()
        {
            if(!_inited)
                await new WaitUntil(()=> _inited);

            if(_currentUser.IsNull())
                return;

            Google.GoogleSignIn.DefaultInstance.SignOut();
            _currentUser = null;
        }
        #endregion

        #region constructor
        public GoogleAuth(iCloudAuthConfig config)
        {
            var webClientID = config.GetConfig<string>(GoogleAuthConfig.WEB_CLIENT_ID);

            Google.GoogleSignIn.Configuration = new Google.GoogleSignInConfiguration()
            {
                RequestIdToken  = true,
                WebClientId     = webClientID
            };
            _inited = true;
        }    
        #endregion    
    }
}
#endif
#endif
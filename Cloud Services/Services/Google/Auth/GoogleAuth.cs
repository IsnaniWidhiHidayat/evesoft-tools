using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using Evesoft;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RollingGlory.FaceApp
{
    [Serializable,HideReferenceObjectPicker]
    public class GoogleAuth : iCloudAuth
    {
        #region private
        private iUser _currentUser;
        private bool _inited;
        #endregion

        #region iCloudAuth
        [ShowInInspector] public bool inited => _inited;
        [ShowInInspector] public iUser currentUser => _currentUser;
        
        public async Task<(iUser,Exception)> Login(IDictionary<string, object> options)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(!_currentUser.IsNull())
                    return (_currentUser,null);
    
                var googleUser = await GoogleSignIn.DefaultInstance.SignIn();
                _currentUser = new User()
                {
                    authType = AuthType.Google,
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

            GoogleSignIn.DefaultInstance.SignOut();
            _currentUser = null;
        }
        #endregion

        #region constructor
        public GoogleAuth(string webClientID)
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration()
            {
                RequestIdToken = true,
                WebClientId = webClientID
            };
            _inited = true;
        }    
        #endregion    
    }
}
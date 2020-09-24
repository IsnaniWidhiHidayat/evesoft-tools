using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
using Firebase;
using System;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    public class FirebaseCloudAuth : iCloudAuth
    {
        #region private
        private bool _inited;
        private bool _initing;
        private iUserAuth _currentUser;
        private FirebaseAuth _auth;
        #endregion

        #region iCloudAuth
        [ShowInInspector] public bool inited => _inited;
        [ShowInInspector] public iUserAuth currentUser => _currentUser;
        public async Task<(iUserAuth,Exception)> Login(IDictionary<string, object> options)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(!_currentUser.IsNull())
                    return (_currentUser,null);
    
                var authtype    = default(CloudAuthType);
                var token       = default(string);
                var outauthtype = default(object);
                var outtoken    = default(object);
                options.TryGetValue(nameof(authtype),out outauthtype);
                options.TryGetValue(nameof(token),out outtoken);
                authtype = (CloudAuthType)outauthtype;
                token    = outtoken as string;
                
                switch(authtype)
                {
                    case CloudAuthType.GoogleSignIn:
                    {
                        var credential  = GoogleAuthProvider.GetCredential(token,null);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                        var accessToken  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = authtype,
                            imageUrl = firebaseUser.PhotoUrl.AbsoluteUri,
                            name = firebaseUser.DisplayName,
                            token    = accessToken,
                            email    = firebaseUser.Email
                        };
                           
                        return (_currentUser,null);
                    }
    
                    case CloudAuthType.Facebook:
                    {  
                        var credential = FacebookAuthProvider.GetCredential(token);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                        token  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = authtype,
                            imageUrl = firebaseUser.PhotoUrl.AbsoluteUri,
                            name = firebaseUser.DisplayName,
                            token    = token,
                            email    = firebaseUser.Email
                        }; 
    
                        return (_currentUser,null);
                    }
    
                    default:
                    {
                        return (null,new Exception("Auth Type Currently Not Supported"));
                    }
                }
            } 
            catch (FirebaseException ex) 
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

            _auth.SignOut();
            _currentUser = null;
        }
        #endregion

        #region constructor
        public FirebaseCloudAuth(iCloudAuthConfig config)
        {
            Init();
        }
        #endregion
        
        #region methods
        private async void Init()
        {
            if(_inited || _initing)
                return;

            _initing = true;
            var status = await FirebaseDependencies.CheckAndFixDependencies();

            switch(status)
            {
                case DependencyStatus.Available:
                {   
                    _auth = FirebaseAuth.DefaultInstance;
                    _initing = false;
                    _inited = true;
                    break;
                }

                default:
                {
                    _initing = false;
                    _inited = false;
                    break;
                }
            }
        } 
        #endregion
    }
}
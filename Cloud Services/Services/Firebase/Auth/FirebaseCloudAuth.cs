using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Auth;
using Evesoft;
using UnityEngine;
using Firebase;
using System;
using Sirenix.OdinInspector;

namespace RollingGlory.FaceApp
{
    [Serializable,HideReferenceObjectPicker]
    public class FirebaseCloudAuth : iCloudAuth
    {
        #region private
        private bool _inited;
        private bool _initing;
        private iUser _currentUser;
        private FirebaseAuth _auth;
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
    
                var authtype    = default(AuthType);
                var token       = default(string);
                var outauthtype = default(object);
                var outtoken    = default(object);
                options.TryGetValue(nameof(authtype),out outauthtype);
                options.TryGetValue(nameof(token),out outtoken);
                authtype = (AuthType)outauthtype;
                token    = outtoken as string;
                
                switch(authtype)
                {
                    case AuthType.Google:
                    {
                        var credential  = GoogleAuthProvider.GetCredential(token,null);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                        var accessToken  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new User()
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
    
                    case AuthType.Facebook:
                    {  
                        var credential = FacebookAuthProvider.GetCredential(token);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                        token  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new User()
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
            catch (Firebase.FirebaseException ex) 
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
        public FirebaseCloudAuth()
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
#if ODIN_INSPECTOR 
#if FIREBASE_AUTH
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
    internal class FirebaseCloudAuth : ICloudAuth
    {
        #region private
        private bool _inited;
        private bool _initing;
        private IUserAuth _currentUser;
        private FirebaseAuth _auth;
        #endregion

        #region iCloudAuth
        [ShowInInspector] public bool inited => _inited;
        [ShowInInspector] public IUserAuth currentUser => _currentUser;
        public async Task<(IUserAuth,Exception)> Login(ICloudAuthOptions options)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(!_currentUser.IsNull())
                    return (_currentUser,null);
    
                var authtype = options.GetOptions<FirebaseCloudAuthType>(FirebaseCloudAuthOptions.LOGIN_TYPE);
            
                if(authtype.IsNull() || authtype == FirebaseCloudAuthType.None)
                    return(null,new ArgumentNullException(nameof(authtype)));

                switch(authtype)
                {
                    case FirebaseCloudAuthType.EmailPassword:
                    {
                        var email    = options.GetOptions<string>(FirebaseCloudAuthOptions.EMAIL);
                        var password = options.GetOptions<string>(FirebaseCloudAuthOptions.PASSWORD);

                        if(email.IsNullOrEmpty())
                            return (null,new ArgumentNullException(FirebaseCloudAuthOptions.EMAIL,"Email is Empty"));

                        if(password.IsNullOrEmpty())
                            return (null,new ArgumentNullException(FirebaseCloudAuthOptions.PASSWORD,"Password is Empty"));

                        //Check email registered
                        var providers = await FirebaseAuth.DefaultInstance.FetchProvidersForEmailAsync(email);
                        if(providers.IsNullOrEmpty())
                            return (null,new FirebaseException(401,"Not Registered"));
                
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password);
                        var accessToken  = await firebaseUser.TokenAsync(false);

                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = CloudAuthType.Firebase,
                            imageUrl = firebaseUser.PhotoUrl?.AbsoluteUri,
                            name     = firebaseUser.DisplayName,
                            email    = firebaseUser.Email,
                            token    = accessToken
                        };

                        return (_currentUser,null);
                    }
                
                    #if GOOGLE_AUTH
                    case FirebaseCloudAuthType.GoogleSignIn:
                    {
                        var accessToken  = options.GetOptions<string>(FirebaseCloudAuthOptions.TOKEN);
                        var credential   = GoogleAuthProvider.GetCredential(accessToken,null);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                            accessToken  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = CloudAuthType.Firebase,
                            imageUrl = firebaseUser.PhotoUrl?.AbsoluteUri,
                            name     = firebaseUser.DisplayName,
                            email    = firebaseUser.Email,
                            token    = accessToken
                        };
                           
                        return (_currentUser,null);
                    }    
                    #endif
                    
                    #if FACEBOOK_AUTH
                    case FirebaseCloudAuthType.Facebook:
                    {  
                        var accessToken  = options.GetOptions<string>(FirebaseCloudAuthOptions.TOKEN);
                        var credential   = FacebookAuthProvider.GetCredential(accessToken);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                            accessToken  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = CloudAuthType.Firebase,
                            imageUrl = firebaseUser.PhotoUrl?.AbsoluteUri,
                            name     = firebaseUser.DisplayName,
                            token    = accessToken,
                            email    = firebaseUser.Email
                        }; 
    
                        return (_currentUser,null);
                    }
                    #endif

                    #if PLAYSERVICE_AUTH
                    case FirebaseCloudAuthType.GooglePlayService:
                    {
                        var accessToken  = options.GetOptions<string>(FirebaseCloudAuthOptions.TOKEN);
                        var credential   = PlayGamesAuthProvider.GetCredential(accessToken);
                        var firebaseUser = await FirebaseAuth.DefaultInstance.SignInWithCredentialAsync(credential);
                            accessToken  = await firebaseUser.TokenAsync(false);
                        
                        _currentUser = new CloudAuthUser()
                        {
                            id       = firebaseUser.UserId,
                            authType = CloudAuthType.Firebase,
                            imageUrl = firebaseUser.PhotoUrl?.AbsoluteUri,
                            name     = firebaseUser.DisplayName,
                            token    = accessToken,
                            email    = firebaseUser.Email
                        }; 
    
                        return (_currentUser,null);
                    }
                    #endif
                    
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
        public FirebaseCloudAuth(ICloudAuthConfig config)
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
#endif
#endif
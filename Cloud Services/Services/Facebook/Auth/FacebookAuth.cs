using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Evesoft;
using Facebook.Unity;
using UnityEngine;
using Sirenix.OdinInspector;

namespace  RollingGlory.FaceApp
{
    [Serializable,HideReferenceObjectPicker]
    public class FacebookAuth : iCloudAuth
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
    
                var perms = new string[]{"public_profile", "email"};
                var accessToken = default(string);
                var errorMessage = default(string);
                
                if(!FB.IsLoggedIn) 
                {
                    var complete = false;
                    FB.LogInWithReadPermissions(perms,
                    (result)=>
                    { 
                        errorMessage = result.Error;
                        accessToken = result.AccessToken.TokenString;
                        complete = true;
                    });
                    await new WaitUntil(()=> complete);
                }
                else
                {
                    accessToken = AccessToken.CurrentAccessToken.TokenString;
                }
    
                if(FB.IsLoggedIn)
                {
                    var isError = false;
                    errorMessage= string.Empty;
                    var id      = string.Empty;
                    var name    = string.Empty;
                    var picture = string.Empty;
                    var email   = string.Empty;
                    var complete = false;
                    FB.API("/me?fields=id,name,picture,email",HttpMethod.GET,
                    graphResult =>
                    {
                        if(!graphResult.Error.IsNullOrEmpty() || graphResult.Cancelled)
                        {
                            isError = true;
                            errorMessage = graphResult.Error;
                        }
                        else
                        {
                            var dic = graphResult.ResultDictionary;
                            var key = nameof(id);
    
                            if(dic.ContainsKey(key))
                                id = dic[key].ToString();
                            
                            key = nameof(name);
                            if(dic.ContainsKey(key))
                                name = dic[key].ToString();

                            key = nameof(email);
                            if(dic.ContainsKey(key))
                                email = dic[key].ToString();
    
                            key = nameof(picture);
                            if(dic.ContainsKey(key))
                            {
                                dic = dic[key] as Dictionary<string,object>;
                                key = "data";
    
                                if(!dic.IsNull() && dic.ContainsKey(key))
                                {
                                    dic = dic[key] as Dictionary<string,object>;
                                    key = "url";
    
                                    if(!dic.IsNull() && dic.ContainsKey(key))
                                        picture = dic[key].ToString();
                                }
                            }
                        }
    
                        complete = true;
                    });
                    
                    await new WaitUntil(()=> complete);
    
                    if(isError)
                    {
                        return (null,new Exception(errorMessage));
                    }
                    else
                    {
                        _currentUser = new User()
                        {
                            id = id,
                            authType = AuthType.Facebook,
                            imageUrl = picture,
                            name    = name,
                            token   = accessToken,
                            email   = email
                        };
                        return (_currentUser,null);
                    }
                }
                else
                {
                    return (default(iUser),new Exception(errorMessage));
                }     
            } 
            catch (System.Exception ex) 
            {
                return (null,ex);
            }
        }
        public void Logout()
        {
            if(_currentUser.IsNull())
                return;

            FB.LogOut();
            _currentUser = null;
        }
        #endregion

        #region constructor
        public FacebookAuth()
        {
            var onFBInitedHandler = default(InitDelegate);
            onFBInitedHandler = ()=>
            {
                if(!FB.IsInitialized)
                {
                    "Failed to Initialize the Facebook SDK".LogError();
                    _inited = false;
                }
                else
                {
                     FB.ActivateApp();
                    _inited = true; 
                }
            };

            //Init Facebook SDK
            if(!FB.IsInitialized)
                FB.Init(onFBInitedHandler);
            else
                _inited = true;
        } 
        #endregion
    }
}
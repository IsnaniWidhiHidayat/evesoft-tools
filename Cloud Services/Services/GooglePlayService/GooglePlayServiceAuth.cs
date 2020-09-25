using System;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Evesoft.CloudService.GooglePlayService
{
    internal class GooglePlayServiceAuth : iCloudAuth
    {
        #region private
        private bool _inited = false;
        private iUserAuth _currentUser;
        #endregion

        #region iCloudAuth
        public bool inited => _inited;
        public iUserAuth currentUser => _currentUser;

        public async Task<(iUserAuth, Exception)> Login(iCloudAuthOptions options = null)
        {
            if(!Application.isMobilePlatform)
                return (null, new PlatformNotSupportedException("Platfrom Not Supported"));
            
            if (!currentUser.IsNull())
                return (currentUser,null);

            var done    = false;
            var success = false; 
            var message = default(string);
            
            PlayGamesPlatform.Instance.Authenticate((complete,error) =>
            {
                success = complete;
                message = error;
                done    = true;
            });
            
            await new WaitUntil(()=> done);

            if (success)
            {
                PlayGamesPlatform.Instance.SetGravityForPopups(Gravity.TOP);
            
                _currentUser = new CloudAuthUser()
                {
                    authType = CloudAuthType.GooglePlayService,
                    email    = PlayGamesPlatform.Instance.GetUserEmail(),
                    name     = PlayGamesPlatform.Instance.GetUserDisplayName(),
                    token    = PlayGamesPlatform.Instance.GetServerAuthCode(),
                    id       = PlayGamesPlatform.Instance.GetUserId(),
                    imageUrl = PlayGamesPlatform.Instance.GetUserImageUrl()
                };

                return (_currentUser,null);
            }
            else
            {
                return (null,new Exception(message));
            }     
        }
        public void Logout()
        {
            PlayGamesPlatform.Instance.SignOut();
        }
        #endregion

        #region constructor
        public GooglePlayServiceAuth(iCloudAuthConfig config)
        {
            var cfg = new PlayGamesClientConfiguration.Builder()
            .RequestServerAuthCode(false)
            .RequestEmail()
            .RequestIdToken()
            .Build();

            PlayGamesPlatform.InitializeInstance(cfg);
            PlayGamesPlatform.Activate();
        }
        #endregion
    }
}
#if ODIN_INSPECTOR 
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudAuthFactory
    {
        private static IDictionary<CloudAuthType,iCloudAuth> auths = new Dictionary<CloudAuthType,iCloudAuth>();

        public static iCloudAuth CreateAuth(iCloudAuthConfig config)
        {
            if(config.IsNull())
                return null;

            var service = config.GetConfig<CloudAuthType>(nameof(CloudService));
            if(auths.ContainsKey(service))
                return auths[service];

            switch(service)
            {
                #if GOOGLE_AUTH
                case CloudAuthType.GoogleSignIn:
                {
                    return auths[service] = new GoogleSignIn.GoogleAuth(config);
                }
                #endif
                
                #if FACEBOOK_AUTH
                case CloudAuthType.Facebook:
                {
                    return auths[service] = new Facebook.FacebookAuth(config);
                }
                #endif

                #if FIREBASE_AUTH
                case CloudAuthType.Firebase:
                {
                    return auths[service] = new Firebase.FirebaseCloudAuth(config);
                }
                #endif
                
                #if PLAYSERVICE_AUTH
                case CloudAuthType.GooglePlayService:
                {
                    return auths[service] = new GooglePlayService.GooglePlayServiceAuth(config);
                }
                #endif
                
                default:
                {
                    "Service Unavailable".LogError();
                    return null;
                }
            }
        }     
    }
}
#endif
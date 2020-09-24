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
                case CloudAuthType.GoogleSignIn:
                {
                    return auths[service] = new GoogleSignIn.GoogleAuth(config);
                }

                case CloudAuthType.Facebook:
                {
                    return auths[service] = new Facebook.FacebookAuth(config);
                }

                case CloudAuthType.Firebase:
                {
                    return auths[service] = new Firebase.FirebaseCloudAuth(config);
                }

                case CloudAuthType.GooglePlayService:
                {
                    return auths[service] = new GooglePlayService.GooglePlayServiceAuth(config);
                }

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }     
    }
}
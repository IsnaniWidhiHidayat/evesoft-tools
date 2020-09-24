using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudAuthConfigFactory
    {
        public static iCloudAuthConfig CreateGoogleAuthOptions(string webclientid)
        {
            return new GoogleSignIn.GoogleAuthConfig(webclientid);
        }
        public static iCloudAuthConfig CreateFacebookAuthOptions()
        {
            return null;
        }
        public static iCloudAuthConfig CreateFirebaseAuthOptions()
        {
            return null;
        }
    }
}
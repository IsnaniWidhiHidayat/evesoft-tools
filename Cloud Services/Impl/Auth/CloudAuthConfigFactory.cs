namespace Evesoft.CloudService
{
    public static class CloudAuthConfigFactory
    {
        public static iCloudAuthConfig CreateGoogleAuthConfig(string webclientid)
        {
            return new GoogleSignIn.GoogleAuthConfig(webclientid);
        }
        public static iCloudAuthConfig CreateFacebookAuthConfig()
        {
            return new Facebook.FacebookAuthConfig();
        }
        public static iCloudAuthConfig CreateFirebaseAuthConfig()
        {
            return new Firebase.FirebaseCloudAuthConfig();
        }
        public static iCloudAuthConfig CreatePlayServiceAuthConfig()
        {
            return new GooglePlayService.GooglePlayServiceAuthConfig();
        }
    }
}
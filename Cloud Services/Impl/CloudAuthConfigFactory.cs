namespace Evesoft.CloudService
{
    public static class CloudAuthConfigFactory
    {
        public static iCloudAuthConfig CreateGoogleAuthOptions(string webclientid)
        {
            var config = new GoogleSignIn.GoogleAuthConfig(webclientid);
            config.configs[nameof(CloudService)] = CloudAuthType.GoogleSignIn;
            return config;
        }
        public static iCloudAuthConfig CreateFacebookAuthOptions()
        {
            var config = new Facebook.FacebookAuthConfig();
            config.configs[nameof(CloudService)] = CloudAuthType.Facebook;
            return config;
        }
        public static iCloudAuthConfig CreateFirebaseAuthOptions()
        {
            var config = new Firebase.FirebaseCloudAuthConfig();
            config.configs[nameof(CloudService)] = CloudAuthType.Firebase;       
            return config;
        }
    }
}
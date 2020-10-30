#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public static class CloudAuthConfigFactory
    {
        #if GOOGLE_AUTH
        public static ICloudAuthConfig CreateGoogleAuthConfig(string webclientid)
        {
            return new GoogleSignIn.GoogleAuthConfig(webclientid);
        }
        #endif
        
        #if FACEBOOK_AUTH
        public static ICloudAuthConfig CreateFacebookAuthConfig()
        {
            return new Facebook.FacebookAuthConfig();
        }
        #endif

        #if FIREBASE_AUTH
        public static ICloudAuthConfig CreateFirebaseAuthConfig()
        {
            return new Firebase.FirebaseCloudAuthConfig();
        }
        #endif
        
        #if PLAYSERVICE_AUTH
        public static ICloudAuthConfig CreatePlayServiceAuthConfig()
        {
            return new GooglePlayService.GooglePlayServiceAuthConfig();
        }
        #endif
    }
}
#endif
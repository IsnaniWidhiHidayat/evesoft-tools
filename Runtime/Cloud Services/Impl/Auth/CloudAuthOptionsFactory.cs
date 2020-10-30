#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public static class CloudAuthOptionsFactory
    { 
        #if FIREBASE_AUTH
        public static ICloudAuthOptions CreateFirebaseWithEmailPassword(string email,string password)
        {
            return new Firebase.FirebaseCloudAuthOptions(email,password);
        }
        #endif

        #if FIREBASE_AUTH && FACEBOOK_AUTH
        public static ICloudAuthOptions CreateFirebaseWithFacebook(string token)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.Facebook,token);
        }
        #endif

        #if FIREBASE_AUTH && GOOGLE_AUTH
        public static ICloudAuthOptions CreateFirebaseWithGoogle(string token)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.GoogleSignIn,token);
        }
        #endif

        #if FIREBASE_AUTH && PLAYSERVICE_AUTH
        public static ICloudAuthOptions CreateFirebaseWithGooglePlayService(string serverAuthCode)
        {
            return new Firebase.FirebaseCloudAuthOptions(Firebase.FirebaseCloudAuthType.GooglePlayService,serverAuthCode);
        }
        #endif
    }
}
#endif
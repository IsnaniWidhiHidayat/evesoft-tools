#if FIREBASE_AUTH
namespace Evesoft.CloudService.Firebase
{
    internal enum FirebaseCloudAuthType
    {
        None,
        EmailPassword,

        #if GOOGLE_AUTH
        GoogleSignIn,
        #endif
        
        #if FACEBOOK_AUTH
        Facebook,
        #endif
        
        #if PLAYSERVICE_AUTH
        GooglePlayService
        #endif
    }
}
#endif
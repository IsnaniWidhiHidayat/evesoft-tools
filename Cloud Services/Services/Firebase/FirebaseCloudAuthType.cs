namespace Evesoft.CloudService.Firebase
{
    internal enum FirebaseCloudAuthType
    {
        None,
        EmailPassword,
        GoogleSignIn,
        
        #if FACEBOOK_AUTH
        Facebook,
        #endif
        
        GooglePlayService
    }
}
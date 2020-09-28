namespace Evesoft.CloudService
{
    public enum CloudAuthType : byte
    {
        None,

        #if GOOGLE_AUTH
        GoogleSignIn,
        #endif
        
        #if PLAYSERVICE_AUTH
        GooglePlayService,
        #endif
        
        #if FIREBASE_AUTH
        Firebase,
        #endif
        
        #if FACEBOOK_AUTH
        Facebook
        #endif
    }
}
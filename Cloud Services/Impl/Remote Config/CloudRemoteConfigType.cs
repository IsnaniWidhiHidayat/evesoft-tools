namespace Evesoft.CloudService
{
    public enum CloudRemoteConfigType : byte
    {
        None,
        #if UNITY_REMOTE_CONFIG
        UnityRemoteConfig,
        #endif
        
        #if FIREBASE_REMOTE_CONFIG
        FirebaseRemoteConfig
        #endif
    }
}
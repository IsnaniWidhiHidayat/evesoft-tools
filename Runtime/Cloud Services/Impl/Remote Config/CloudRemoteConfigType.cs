#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public enum CloudRemoteConfigType : byte
    {
        None,
        #if UNITY_REMOTE_CONFIG
        UnityRemoteConfig,
        #endif
        
        #if FIREBASE_REMOTE_CONFIG || FIREBASE_REALTIME_DATABASE
        FirebaseRemoteConfig
        #endif
    }
}
#endif
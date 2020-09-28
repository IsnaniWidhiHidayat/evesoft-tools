#if ODIN_INSPECTOR 
#if FIREBASE_REMOTE_CONFIG || FIREBASE_REALTIME_DATABASE
namespace Evesoft.CloudService.Firebase
{
    public enum FirebaseCloudRemoteConfigType
    {
        #if FIREBASE_REMOTE_CONFIG
        RemoteConfig,
        #endif
        
        #if FIREBASE_REALTIME_DATABASE
        RealtimeDatabase
        #endif
    }
}
#endif
#endif
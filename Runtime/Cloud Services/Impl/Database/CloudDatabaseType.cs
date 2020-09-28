#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public enum CloudDatabaseType : byte
    {
        None,
        
        #if FIREBASE_REALTIME_DATABASE
        FirebaseRealtimeDatabase
        #endif
    }
}
#endif
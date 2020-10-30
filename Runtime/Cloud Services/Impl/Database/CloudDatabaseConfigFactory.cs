#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public static class CloudDatabaseConfigFactory
    {
        #if FIREBASE_REALTIME_DATABASE
        public static ICloudDatabaseConfig CreateFirebaseDatabaseConfig(string url = null)
        {
            return new Firebase.FirebaseCloudDatabaseConfig(url);
        }   
        #endif   
    }
}
#endif
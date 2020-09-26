namespace Evesoft.CloudService
{
    public static class CloudDatabaseConfigFactory
    {
        #if FIREBASE_REALTIME_DATABASE
        public static iCloudDatabaseConfig CreateFirebaseDatabaseConfig(string url = null)
        {
            return new Firebase.FirebaseCloudDatabaseConfig(url);
        }   
        #endif   
    }
}
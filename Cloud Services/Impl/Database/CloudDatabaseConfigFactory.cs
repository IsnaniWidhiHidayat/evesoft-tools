namespace Evesoft.CloudService
{
    public static class CloudDatabaseConfigFactory
    {
        public static iCloudDatabaseConfig CreateFirebaseDatabaseConfig(string url = null)
        {
            return new Firebase.FirebaseCloudDatabaseConfig(url);
        }      
    }
}
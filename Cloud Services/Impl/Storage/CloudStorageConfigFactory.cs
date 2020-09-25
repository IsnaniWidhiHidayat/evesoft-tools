
namespace Evesoft.CloudService
{
    public static class CloudStorageConfigFactory
    {
        public static iCloudStorageConfig CreateFirebaseStorageConfig(string url = null)
        {
            return new Firebase.FirebaseCloudStorageConfig(url);
        }      
    }
}

namespace Evesoft.CloudService
{
    public static class CloudStorageConfigFactory
    {
        #if FIREBASE_STORAGE
        public static iCloudStorageConfig CreateFirebaseStorageConfig(string url = null)
        {
            return new Firebase.FirebaseCloudStorageConfig(url);
        }
        #endif      
    }
}
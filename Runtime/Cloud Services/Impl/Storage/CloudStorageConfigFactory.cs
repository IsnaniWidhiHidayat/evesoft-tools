#if ODIN_INSPECTOR 

namespace Evesoft.CloudService
{
    public static class CloudStorageConfigFactory
    {
        #if FIREBASE_STORAGE
        public static ICloudStorageConfig CreateFirebaseStorageConfig(string url = null)
        {
            return new Firebase.FirebaseCloudStorageConfig(url);
        }
        #endif      
    }
}
#endif
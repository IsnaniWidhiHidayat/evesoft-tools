#if ODIN_INSPECTOR 
namespace Evesoft.CloudService
{
    public enum CloudStorageType
    {
        None,

        #if FIREBASE_STORAGE
        FirebaseStorage
        #endif
    }
}
#endif
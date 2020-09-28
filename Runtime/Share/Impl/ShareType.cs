#if ODIN_INSPECTOR 
namespace Evesoft.Share
{
    public enum ShareType
    {
        #if NATIVE_SHARE
        NativeShare,
        #endif
    }
}
#endif
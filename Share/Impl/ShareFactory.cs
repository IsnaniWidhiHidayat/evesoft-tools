namespace Evesoft.Share
{
    public static class ShareFactory 
    {
        public static iShare CreateShare(ShareType type)
        {
            switch(type)
            {
                #if NATIVE_SHARE
                case ShareType.NativeShare:
                {
                    return new NativeShare.NativeShare();
                }
                #endif

                default:
                {
                    "Share Unavailable".LogError();
                    return null;
                }
            }
        }    
    }
}
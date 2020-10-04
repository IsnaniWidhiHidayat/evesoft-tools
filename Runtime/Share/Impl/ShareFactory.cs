#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.Share
{
    public static class ShareFactory 
    {
        private static IDictionary<ShareType,iShare> _shareService = new Dictionary<ShareType,iShare>();

        public static iShare Create(ShareType type)
        {
            var share = Get(type);
            if(!share.IsNull())
                return share;

            switch(type)
            {
                #if NATIVE_SHARE
                case ShareType.NativeShare:
                {
                    return  _shareService[type] = new NativeShare.NativeShare();
                }
                #endif

                default:
                {
                    "Share Unavailable".LogError();
                    return null;
                }
            }
        }  
        public static iShare Get(ShareType type)
        {
            if(_shareService.ContainsKey(type))
                return _shareService[type];

            return null;
        }  
        public static async Task<iShare> GetAsync(ShareType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
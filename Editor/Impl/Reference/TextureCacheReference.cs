#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;

namespace Evesoft.Editor.Reference
{
    [HideReferenceObjectPicker]
    public class TextureCacheReference
    {
        #if CACHE_TEXTURE2D
        [ShowInInspector,ShowIf(nameof(ShowCache))]
        private object cache => Evesoft.Cache.Texture2DCache.GetCaches();

        private bool ShowCache()
        {
            return !Evesoft.Cache.Texture2DCache.GetCaches().IsNullOrEmpty();
        }
        #endif
    }
}
#endif
#if CACHE_TEXTURE2D
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Evesoft.Cache;

namespace Evesoft.Editor.Cache
{
    [System.Serializable,HideReferenceObjectPicker]
    public class TextureCachedEditor : iGroupEditor
    {
        #region Field
        [ShowInInspector,HideLabel,ReadOnly,HideIf(nameof(IsEmptyCache))]
        private Dictionary<string,iCache<Texture2DCacheData>> caches;
        #endregion

        private bool IsEmptyCache()
        {
            return caches.IsNullOrEmpty();
        }

        #region iGroupEditor
        public string name => "Texture Cache";
        public void Refresh()
        {
            caches = Texture2DCache.GetCaches();
        }
        public void OnScriptReloaded()
        {
            Texture2DCache.RemoveAllCaches();
        }
        public void OnWindowClicked()
        {
            Refresh();
        }
        #endregion
    }
}
#endif
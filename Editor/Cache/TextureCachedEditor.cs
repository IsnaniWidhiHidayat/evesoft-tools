#if CACHE_TEXTURE2D

using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;

namespace Evesoft.Editor.Cache
{
    public class TextureCachedEditor : OdinEditorWindow
    {
        #region const
        const string path = "Tools/EveSoft/Cache/Textures";
        #endregion

        #region static
        private static TextureCachedEditor instance;

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void Reload()
        {
            Texture2DCache.RemoveAllCaches();
        }
        
        [MenuItem(path)]
        static void ShowWindow()
        {
            var window = GetWindow<TextureCachedEditor>();

            if(!UnityEditor.EditorApplication.isPlaying)
            {
                window.caches?.Clear();
                window.caches = null;
            }    
            else
            {
                window.caches = Texture2DCache.GetCaches();
            }
    
            window.Show();
        }    
        #endregion

        #region Field
        [HideLabel,ReadOnly,HideIf(nameof(IsEmptyCache))]
        public Dictionary<string,iCache<Texture2DCacheData>> caches;

        private bool IsEmptyCache()
        {
            return caches.IsNullOrEmpty();
        }
        #endregion

        #region callback
        protected override void OnEnable()
        {
            base.OnEnable();
            instance = this;
        }  
        #endregion
    }
}
#endif
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;
using Evesoft;
using UnityEngine;

namespace Evesoft.Cache.Editor
{
    public class TextureCachedBrowser : OdinEditorWindow
    {
        #region const
        const string path = "Tools/EveSoft/Cache/Textures";
        #endregion

        #region static
        [MenuItem(path)]
        static void ShowWindow()
        {
            var window = GetWindow<TextureCachedBrowser>();

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
    }
}
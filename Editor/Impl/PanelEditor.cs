using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Evesoft.Editor
{
    public class PanelEditor : OdinEditorWindow
    {
        #region const
        const string path = "Tools/EveSoft/Panels";
        #endregion

        #region static
        private static PanelEditor instance;

        [MenuItem(path)]
        private static void ShowWindow()
        {
            var window = GetWindow<PanelEditor>();
            window.Refresh();
            window.Show();
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptReload()
        {
            if(instance.IsNull() || instance.editors.IsNullOrEmpty())
                return;

            foreach (var editor in instance.editors)
                editor?.OnScriptReloaded();

        }
        #endregion
    
        #region private

        [ShowInInspector,HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden),TabGroup(nameof(Bridges))]
        private iGroupEditor Bridges;

        [ShowInInspector,HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden),TabGroup(nameof(Scenes))]
        private iGroupEditor Scenes;

        [ShowInInspector,HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden),TabGroup(nameof(DefineSymbol))]
        private iGroupEditor DefineSymbol;

        #if CACHE_TEXTURE2D
        [ShowInInspector,HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden),TabGroup(nameof(TextureCache))]
        private iGroupEditor TextureCache;
        #endif
        
        private IList<iGroupEditor> editors;
        #endregion

        #region methods
        private void Refresh()
        {
            if(editors.IsNullOrEmpty())
                return;

            foreach (var editor in editors)
                editor?.OnScriptReloaded();
        }
        #endregion

        #region constructor
        public PanelEditor()
        {
            Bridges         = new Bridge.BridgeEditor();
            Scenes          = new Scene.SceneEditor();
            DefineSymbol    = new ScriptingDefineSymbol.ScriptingDefineSymbolEditor();
            
            #if CACHE_TEXTURE2D
            TextureCache    = new Cache.TextureCachedEditor();
            #endif
            

            editors = new List<iGroupEditor>()
            {
                Bridges     ,
                Scenes      ,
                DefineSymbol,
                #if CACHE_TEXTURE2D
                TextureCache,
                #endif 
            };
        }
        #endregion
    
        #region callback
        protected override void OnEnable()
        {
            base.OnEnable();
            instance  = this;
        }
        protected override void OnGUI()
        {
            base.OnGUI();

            if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                if(editors.IsNullOrEmpty())
                    return;

                foreach (var editor in editors)
                    editor?.OnWindowClicked();
            } 
        }  
        #endregion
    }
}
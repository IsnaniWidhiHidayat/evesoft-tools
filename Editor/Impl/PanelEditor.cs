#if ODIN_INSPECTOR 
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
        const string path = "Tools/EveSoft";
        #endregion

        #region static
        private static PanelEditor instance;

        [MenuItem(path)]
        private static void ShowWindow()
        {
            instance = GetWindow<PanelEditor>(nameof(Evesoft),true,typeof(EditorWindow));
            instance.Refresh();
            instance.Show();
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

        [ShowInInspector,HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden),TabGroup(nameof(Dir))]
        private iGroupEditor Dir;
 
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
            Dir             = new Directory.DirectoryEditor();

            editors = new List<iGroupEditor>()
            {
                Bridges     ,
                Scenes      ,
                DefineSymbol,
                Dir,
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
            
            if(editors.IsNullOrEmpty())
                return;

            foreach (var editor in editors)
            {
                if(Event.current.type == EventType.MouseDown)
                {
                    if(Event.current.button == 0)
                    {
                        editor?.OnWindowClicked();
                    }
                }
                else
                {
                    editor?.OnGUI();
                } 
            }
        }  
        #endregion
    }
}

#else
using UnityEditor;
using UnityEngine;

namespace Evesoft.Editor
{
    public static class PanelEditor
    {
        #region const
        const string path = "Tools/EveSoft";
        #endregion

        [MenuItem(path)]
        private static void ShowWindow()
        {
            EditorUtility.DisplayDialog("Message","Required Odin Inspector","Ok");
        }
    }
}
#endif
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Evesoft.Editor.ScriptingDefineSymbol
{
    public class ScriptingDefineSymbolEditor : OdinEditorWindow
    {
        #region const
        const string path  = "Tools/EveSoft/Browser/Scripting Define Symbol";
        #endregion

        #region static
        public static ScriptingDefineSymbolEditor instance;
        [MenuItem(path)]
        public static void ShowWindow()
        {
            var window = GetWindow<ScriptingDefineSymbolEditor>();
            window.Refresh();
            window.Show();
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void Reload()
        {
            instance?.Refresh();    
        }
        #endregion

        
        #region field
        [ShowInInspector,ShowIf(nameof(ShowSymbols)),ListDrawerSettings(Expanded = true)]
        private IList<string> _symbols;
        #endregion

        private void Refresh()
        {
            _symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
        }
        
        [Button]
        private void Save()
        {
            ScriptingDefineSymbolUtility.SaveDefineSymbol(_symbols);
        }
       
        private bool ShowSymbols()
        {
            return !EditorApplication.isCompiling && !_symbols.IsNullOrEmpty();
        }   
        protected override void OnEnable()
        {
            base.OnEnable();
            instance = this;
        }
        protected override void OnGUI()
        {
            base.OnGUI();

            if(Event.current.type == EventType.MouseDown && Event.current.button == 0) {
                Refresh();
            }
        }
    }
}
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;

namespace Evesoft.Editor.ScriptingDefineSymbol
{
    [System.Serializable,HideReferenceObjectPicker]
    public class ScriptingDefineSymbolEditor : iGroupEditor
    {
        #region field
        [InfoBox("Be careful change this, because can break your code",InfoMessageType.Warning)]
        [ShowInInspector,ShowIf(nameof(ShowSymbols)),ListDrawerSettings(Expanded = true)]
        private IList<string> _symbols;
        #endregion

        #region iGroupEditor
        public string name => "Define Symbol";

        public void Refresh()
        {
            _symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
        } 
        public void OnScriptReloaded()
        {
            Refresh();
        }
        #endregion

        #region methods
        [Button,PropertyOrder(-1)]
        private void Apply()
        {
            ScriptingDefineSymbolUtility.SaveDefineSymbol(_symbols);
        }
        private bool ShowSymbols()
        {
            return !EditorApplication.isCompiling && !_symbols.IsNullOrEmpty();
        }   
        public void OnWindowClicked()
        {
            Refresh();
        }
        public void OnGUI()
        {
            
        }
        #endregion
    }
}
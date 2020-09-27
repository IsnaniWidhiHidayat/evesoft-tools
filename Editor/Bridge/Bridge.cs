using Sirenix.OdinInspector;
using System.Collections.Generic;
using Evesoft.Editor.ScriptingDefineSymbol;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker,Toggle(nameof(_isEnable),CollapseOthersOnExpand = false)]
    internal class Bridge
    {
        #region private
        private string _name;
        private IList<string> _symbols;
        public bool _isEnable;
        #endregion

        #region property
        internal bool isEnable => _isEnable;
        internal string name => _name;
        internal IList<string> symbols => _symbols;
        #endregion

        #region methods
        //[Button("$GetStatusName",ButtonSizes.Medium),GUIColor(nameof(GetColorByStatus))]//,HorizontalGroup]
        // private void ToggleEnable()
        // {
        //     if(!_isEnable)
        //         ScriptingDefineSymbolEditor.AddDefineSymbol(_symbols);
        //     else
        //         ScriptingDefineSymbolEditor.RemoveDefineSymbol(_symbols);
        // }
        // private string GetStatusName(){
        //     return _isEnable? "Enable" : "Disable";
        // }
        // private Color GetColorByStatus()
        // {
        //     return _isEnable ? Color.green : Color.red;
        // }
        internal void Refresh()
        {
            _isEnable = ScriptingDefineSymbolUtility.ContainSymbol(_symbols);
        }
        #endregion

        #region constructor
        internal Bridge(string description,params string[] symbols)
        {
            _name    = description;
            _symbols = new List<string>(symbols);
        }   
        #endregion
    }
}
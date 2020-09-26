using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Editor
{
    [HideReferenceObjectPicker]
    internal class Bridge
    {
        [ShowInInspector,DisplayAsString,HideLabel,HorizontalGroup]
        internal string Name;
        internal string symbol;

        private bool _isActive;

        [Button("$GetStatusName",ButtonSizes.Medium),GUIColor(nameof(GetColorByStatus)),HorizontalGroup]
        private void ToggleEnable()
        {
            if(_isActive)
                ScriptingDefineSymbolEditor.AddDefineSymbol(symbol);
            else
                ScriptingDefineSymbolEditor.RemoveDefineSymbol(symbol);
        }
        private string GetStatusName(){
            return _isActive? "Enable" : "Disable";
        }
        private Color GetColorByStatus()
        {
            return _isActive ? Color.green : Color.red;
        }
        public void Refresh()
        {
            _isActive = ScriptingDefineSymbolEditor.ContainSymbol(symbol);
        }

        public Bridge(string description,string symbol)
        {
            this.Name = description;
            this.symbol  = symbol;
        }
    }
}
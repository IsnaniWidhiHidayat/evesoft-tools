using Sirenix.OdinInspector;
using System.Collections.Generic;
using Evesoft.Editor.ScriptingDefineSymbol;
using System;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker,Toggle(nameof(_isEnable),CollapseOthersOnExpand = false)]
    public class Bridge
    {
        #region private
        private string _name;
        private IList<string> _symbols;
        public bool _isEnable;
        private bool _isPluginInstalled;

        [ShowInInspector,ShowIf(nameof(_reference)),HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object _reference;
        #endregion

        #region property
        internal bool isEnable => _isEnable;
        internal string name => _name;
        internal IList<string> symbols => _symbols;
        internal bool isPluginInstalled => _isPluginInstalled;
        #endregion

        #region methods
        internal void Refresh()
        {
            _isEnable = ScriptingDefineSymbolUtility.ContainSymbol(_symbols);

            if(!_reference.IsNull())
            {
                (_reference as iRefresh)?.Refresh();
            }
        }
        #endregion

        #region constructor
        internal Bridge(string description,string pluginNamespace,string symbol,object reference = null)
        {
            _name    = description;
            _symbols = new List<string>();
            _symbols.Add(symbol);
           
            _isPluginInstalled = !pluginNamespace.IsNullOrEmpty()? NamespaceUtility.IsNamespaceExists(pluginNamespace) : true;
            _reference = reference;
        } 
        #endregion
    }
}
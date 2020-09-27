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
        }
        #endregion

        #region constructor
        internal Bridge(string description,string pluginNamespace,params string[] symbols)
        {
            _name    = description;
            _symbols = new List<string>(symbols);
           
            _isPluginInstalled = !pluginNamespace.IsNullOrEmpty()? NamespaceUtility.IsNamespaceExists(pluginNamespace) : true;
        }   
        #endregion
    }
}
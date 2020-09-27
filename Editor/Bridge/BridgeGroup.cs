using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker]
    internal class BridgeGroup
    {
        #region field
        [ShowInInspector,FoldoutGroup("$" + nameof(_grpName)),ListDrawerSettings(Expanded = true,IsReadOnly = true,ShowItemCount = false,ListElementLabelName="name")]
        private IList<Bridge> _bridges;
        #endregion
        
        #region private
        private string _grpName;
        private IList<string> _symbols;
        #endregion

        #region property
        internal IList<Bridge> bridges => _bridges;
        internal IList<string> symbols => _symbols;
        #endregion

        #region methods
        internal void Refresh()
        {
            if(_bridges.IsNullOrEmpty())
                return;

            foreach (var plugin in _bridges)
                plugin?.Refresh();   
        }
        #endregion
        
        #region constructor
        internal BridgeGroup(string grpName,IList<Bridge> bridges)
        {
            _grpName = grpName;
            _bridges = bridges;
            var symbols = new List<string>();

            if(!_bridges.IsNullOrEmpty())
                foreach (var bridge in _bridges)
                    if(!bridge.symbols.IsNullOrEmpty())
                        foreach (var symbol in bridge.symbols)
                            if(!symbols.Contains(symbol))
                                symbols.Add(symbol);
                    
            _symbols = symbols;
        }
        #endregion
    }
}
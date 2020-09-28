#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Evesoft.Editor.ScriptingDefineSymbol;
using System;
using UnityEngine;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker,Toggle(nameof(_isEnable),CollapseOthersOnExpand = false)]
    public class Bridge:iRefresh
    {
        #region private
        public bool _isEnable;
        private string _name;
        private string _symbol;


        private bool ShowRequired => !_required.IsNullOrEmpty();
        [ShowInInspector,ShowIf(nameof(ShowRequired)),ListDrawerSettings(IsReadOnly = true,Expanded = true)]
        [InfoBox("Current Platfrom Is Not Supported",InfoMessageType.Warning,nameof(isPlatformUnSupported))]
        private IList<Prequested> _required;

        [ShowInInspector,HideLabel,DisplayAsString(false),FoldoutGroup("API")]
        private string _guide = "Cooming Soon";

        [ShowInInspector,ShowIf(nameof(_reference)),HideLabel,InlineEditor(objectFieldMode:InlineEditorObjectFieldModes.Hidden)]
        private object _reference;
        #endregion

        #region property
        internal bool isEnable => _isEnable;
        internal string name => _name;
        internal string symbol => _symbol;
        internal bool isPlatformSupported 
        {
            get
            {
                var result = true;
                if(!_required.IsNullOrEmpty())
                {
                    foreach (var require in _required)
                        result &= require.isPlatformSupported;
                }
                return result;
            }
        }
        internal bool isPlatformUnSupported => !isPlatformSupported;
        internal bool isInstalled
        {
            get
            {
                var result = true;
                if(!_required.IsNullOrEmpty())
                {
                    foreach (var require in _required)
                        result &= require.isInstalled;
                }
                return result;
            }
        }
        #endregion

        #region methods
        public void Refresh()
        {
            _isEnable = ScriptingDefineSymbolUtility.ContainSymbol(_symbol);

            if(!_reference.IsNull())
            {
                (_reference as iRefresh)?.Refresh();
            }   
        }
        public void RefreshRequired()
        {
            if(!_required.IsNullOrEmpty())
                foreach (var require in _required)
                    require?.Refresh();
        }
        public void AddHowToUse(string guide)
        {
            _guide = guide;
        }
        #endregion

        #region constructor
        internal Bridge(string name,string symbol,object reference = null,params Prequested[] required)
        {
            _name       = name;
            _symbol     = symbol;
            _required   = required;
            _reference = reference;
        } 
        #endregion
    }
}
#endif
﻿using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Evesoft.Editor
{
    [HideReferenceObjectPicker]
    internal class BridgeGroup
    {
        #region field
        [ShowInInspector,FoldoutGroup("$grpName"),ListDrawerSettings(Expanded = true,IsReadOnly = true,ShowItemCount = false)]
        internal List<Bridge> plugins;
        #endregion
        
        #region private
        private string grpName;
        #endregion

        public void Refresh()
        {
            if(plugins.IsNullOrEmpty())
                return;

            foreach (var plugin in plugins)
                plugin?.Refresh();   
        }

        #region constructor
        public BridgeGroup(string grpName,Bridge[] plugins)
        {
            this.grpName = grpName;
            this.plugins = new List<Bridge>();
            this.plugins.AddRange(plugins);
        }
        #endregion
    }
}
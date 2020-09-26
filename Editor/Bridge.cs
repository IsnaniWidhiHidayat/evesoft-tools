using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Editor
{
    [HideReferenceObjectPicker]
    internal class Bridge
    {
        [ShowInInspector,DisplayAsString,HideLabel,HorizontalGroup]
        internal string Name;
        internal string brigdeName;

        private bool _isActive;

        [Button("$GetStatusName",ButtonSizes.Medium),GUIColor(nameof(GetColorByStatus)),HorizontalGroup]
        private void Status()
        {

        }
        private string GetStatusName(){
            return _isActive? "Enable" : "Disable";
        }
        private Color GetColorByStatus()
        {
            return _isActive ? Color.green : Color.red;
        }

        public Bridge(string description,string bridgeName)
        {
            this.Name = description;
            this.brigdeName  = bridgeName;
        }
    }
}
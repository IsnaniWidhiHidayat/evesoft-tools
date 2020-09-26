using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using System.Collections.Generic;
using Evesoft;
using UnityEngine;

namespace Evesoft.Cache.Editor
{
    internal class BridgeActivator : OdinEditorWindow
    {
        #region const
        const string path = "Tools/EveSoft/Bridges";
        #endregion

        #region static
        [MenuItem(path)]
        static void ShowWindow()
        {
            var window = GetWindow<BridgeActivator>();
            window.Show();
        }
        #endregion

        #region Field

        [ShowInInspector,HideLabel]
        private BridgeGroup Ads = new BridgeGroup(nameof(Ads),new Bridge[]
        {
            new Bridge("Admob","ADMOB"),
            new Bridge("UnityAds","UNITY_ADS")
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Auth = new BridgeGroup(nameof(Auth),new Bridge[]
        {
            new Bridge("Google","GOOGLE"),
            new Bridge("Facebook","FACEBOOK"),
            new Bridge("Play Service","PLAYSERVICE"),
            new Bridge("Firebase","FIREBASE")
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Cache = new BridgeGroup(nameof(Cache),new Bridge[]
        {
            new Bridge("Texture2D","CACHE_TEXTURE2D"),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Database = new BridgeGroup(nameof(Database),new Bridge[]
        {
            new Bridge("Firebase","FIREBASE_DATABASE"),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup IAP = new BridgeGroup("In App Purchase",new Bridge[]
        {
            new Bridge("Unity IAP","UNITY_IAP"),
        });
     
        [ShowInInspector,HideLabel]
        private BridgeGroup localize = new BridgeGroup(nameof(localize),new Bridge[]
        {
            new Bridge("Localize","LOCALIZE"),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup RemoteConfig = new BridgeGroup(nameof(RemoteConfig),new Bridge[]
        {
            new Bridge("Firebase Remote Config","FIREBASE_REMOTECONFIG"),
            new Bridge("Unity Remote Config","UNITY_REMOTECONFIG"),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Storage = new BridgeGroup(nameof(Storage),new Bridge[]
        {
            new Bridge("Firebase Storage","FIREBASE_STORAGE"),
        });
        #endregion
    }

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

    [HideReferenceObjectPicker]
    internal class BridgeGroup
    {
        [ShowInInspector,FoldoutGroup("$grpName"),ListDrawerSettings(Expanded = true,IsReadOnly = true,ShowItemCount = false)]
        internal List<Bridge> plugins;
        private string grpName;

        public BridgeGroup(string grpName,Bridge[] plugins)
        {
            this.grpName = grpName;
            this.plugins = new List<Bridge>();
            this.plugins.AddRange(plugins);
        }
    }
}
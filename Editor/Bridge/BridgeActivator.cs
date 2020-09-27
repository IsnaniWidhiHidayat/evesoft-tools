using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Evesoft.Editor.ScriptingDefineSymbol;

namespace Evesoft.Editor.Bridge
{
    internal class BridgeActivator : OdinEditorWindow
    {
        #region const
        const string path = "Tools/EveSoft/Bridges";
        #endregion

        #region static
        private static BridgeActivator instance;

        [MenuItem(path)]
        private static void ShowWindow()
        {
            var window = GetWindow<BridgeActivator>();
            window.Refresh();
            window.Show();
        }
        
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptReload()
        {
            instance?.Refresh();    
        }
        #endregion
       
        #region private
        [ShowInInspector,HideLabel,DisableIf(nameof(IsCompiling))]
        private BridgeGroup Ads,Auth,Cache,Database,IAP,localize,RemoteConfig,Storage;

        private List<Bridge> bridges;
        #endregion 
        
        private bool IsCompiling()
        {
            return EditorApplication.isCompiling;
        }
        private void ApplyBridges()
        {
            var symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
        
            foreach (var bridge in bridges)
            {
                if(!bridge.symbols.IsNullOrEmpty())
                {
                    foreach (var symbol in bridge.symbols)
                    {
                        if(bridge.isEnable && !symbols.Contains(symbol))
                            symbols.Add(symbol);   
                        else if(!bridge.isEnable)
                            symbols.Remove(symbol); 
                    }
                }
            
            }

            ScriptingDefineSymbolUtility.SaveDefineSymbol(symbols);
        }
        private void Refresh()
        {
            foreach (var bridge in bridges)
                bridge?.Refresh();
        }
       
        #region constructor
        internal BridgeActivator()
        {
            Ads = new BridgeGroup(nameof(Ads),new Bridge[]
            {
                new Bridge("Admob",BridgeSymbol.ADMOB),
                new Bridge("UnityAds",BridgeSymbol.UNITY_ADS)
            });

            Auth = new BridgeGroup(nameof(Auth),new Bridge[]
            {
                new Bridge("Google",BridgeSymbol.GOOGLE_AUTH),
                new Bridge("Facebook",BridgeSymbol.FACEBOOK_AUTH),
                new Bridge("Play Service",BridgeSymbol.PLAYSERVICE_AUTH),
                new Bridge("Firebase",BridgeSymbol.FIREBASE_AUTH)
            });

            Cache = new BridgeGroup(nameof(Cache),new Bridge[]
            {
                new Bridge("Texture2D",BridgeSymbol.CACHE_TEXTURE2D),
            });

            Database = new BridgeGroup(nameof(Database),new Bridge[]
            {
                new Bridge("Firebase",BridgeSymbol.FIREBASE_REALTIME_DATABASE),
            });

            IAP = new BridgeGroup("In App Purchase",new Bridge[]
            {
                new Bridge("Unity IAP",BridgeSymbol.UNITY_IAP),
            });

            localize = new BridgeGroup(nameof(localize),new Bridge[]
            {
                new Bridge("Localize",BridgeSymbol.LOCALIZE)
            });

            RemoteConfig = new BridgeGroup(nameof(RemoteConfig),new Bridge[]
            {
                new Bridge("Firebase Realtime Database",BridgeSymbol.FIREBASE_REALTIME_DATABASE),
                new Bridge("Firebase Remote Config",BridgeSymbol.FIREBASE_REMOTE_CONFIG),
                new Bridge("Unity Remote Config",BridgeSymbol.UNITY_REMOTE_CONFIG),
            });

            Storage = new BridgeGroup(nameof(Storage),new Bridge[]
            {
                new Bridge("Firebase Storage",BridgeSymbol.FIREBASE_STORAGE),
            });
        
            bridges = new List<Bridge>();
            bridges.AddRange(Ads.bridges);
            bridges.AddRange(Auth.bridges);
            bridges.AddRange(Cache.bridges);
            bridges.AddRange(Database.bridges);
            bridges.AddRange(IAP.bridges);
            bridges.AddRange(localize.bridges);
            bridges.AddRange(RemoteConfig.bridges);
            bridges.AddRange(Storage.bridges);
        }
        #endregion

        #region callback
        protected override void OnEnable()
        {
            base.OnEnable();
            instance = this;
        }      
        protected override void OnGUI()
        {
            base.OnGUI();

            if(Event.current.type == EventType.MouseDown && Event.current.button == 0)
            {
                ApplyBridges();
            } 
        }  
        #endregion 
    }
}
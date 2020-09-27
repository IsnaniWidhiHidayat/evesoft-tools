using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Evesoft.Editor.ScriptingDefineSymbol;
using UnityEditor;

namespace Evesoft.Editor.Bridge
{
    [Serializable,HideReferenceObjectPicker]
    public class BridgeEditor : iGroupEditor
    {
        #region private
        private List<Bridge> bridges;

        [ShowInInspector,HideLabel,DisableIf("@EditorApplication.isCompiling")]
        private Bridge Admob,
        UnityAds,
        GoogleSignIn,
        FacebookSignIn,
        PlayServiceSignIn,
        FirebaseSignIn,
        TextureCache,
        FirebaseRealtimeDatabase,
        UnityIAP,
        Localize,
        FirebaseRemoteConfigRealtimeDatabase,
        FirebaseRemoteConfig,
        UnityRemoteConfig,
        FirebaseStorage;
        #endregion

        #region iGroupEditor
        public string name => nameof(BridgeEditor);
        public void Refresh()
        {
            foreach (var bridge in bridges)
                bridge?.Refresh();
        }
        public void OnScriptReloaded()
        {
            Refresh();
        }
        public void OnWindowClicked()
        {
            Refresh();
        }
        #endregion

        [Button,PropertyOrder(-1),DisableIf("@EditorApplication.isCompiling")]
        public void Apply()
        {
            var symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
            var pluginNotInstalled = default(IList<Bridge>);

            foreach (var bridge in bridges)
            {
                if(!bridge.symbols.IsNullOrEmpty())
                {
                    foreach (var symbol in bridge.symbols)
                    {
                        if(bridge.isEnable)
                        {
                            if(!bridge.isPluginInstalled)
                            {
                                if(pluginNotInstalled.IsNull())
                                    pluginNotInstalled = new List<Bridge>();

                                pluginNotInstalled.Add(bridge);
                            }
                            else if(!symbols.Contains(symbol))
                            {
                                symbols.Add(symbol); 
                            }
                        }      
                        else if(!bridge.isEnable)
                        {
                            symbols.Remove(symbol); 
                        }
                    }
                }
            
            }

            if(pluginNotInstalled.IsNullOrEmpty())
            {
                ScriptingDefineSymbolUtility.SaveDefineSymbol(symbols);
            }
            else
            {
                var message = "Please install plugin first before using bridge : ";
                
                for (int i = 0; i < pluginNotInstalled.Count; i++)
                {
                    var name      = pluginNotInstalled[i].name;
                    var separator = i<pluginNotInstalled.Count-1?",":"";
                    message += string.Format("{0}{1}",name,separator);
                }

                EditorUtility.DisplayDialog("Required",message,"ok");
                Refresh();
            }    
        }

        #region constructor
        public BridgeEditor()
        {
            Admob                                = new Bridge("Admob","GoogleMobileAds.Api",BridgeSymbol.ADMOB);
            UnityAds                             = new Bridge("Unity Ads","UnityEngine.Advertisements",BridgeSymbol.UNITY_ADS);
            GoogleSignIn                         = new Bridge("Google Sign In","Google",BridgeSymbol.GOOGLE_AUTH);
            FacebookSignIn                       = new Bridge("Facebook","Facebook.Unity",BridgeSymbol.FACEBOOK_AUTH);
            PlayServiceSignIn                    = new Bridge("Play Service","GooglePlayGames",BridgeSymbol.PLAYSERVICE_AUTH);
            FirebaseSignIn                       = new Bridge("Firebase Auth","Firebase.Auth",BridgeSymbol.FIREBASE_AUTH);
            TextureCache                         = new Bridge("Cache - Texture2D",null,BridgeSymbol.CACHE_TEXTURE2D);
            FirebaseRealtimeDatabase             = new Bridge("Firebase Database","Firebase.Database",BridgeSymbol.FIREBASE_REALTIME_DATABASE);
            UnityIAP                             = new Bridge("Unity IAP","UnityEngine.Purchasing",BridgeSymbol.UNITY_IAP);
            Localize                             = new Bridge("Localize",null,BridgeSymbol.LOCALIZE);
            FirebaseRemoteConfigRealtimeDatabase = new Bridge("Firebase Database","Firebase.Database",BridgeSymbol.FIREBASE_REALTIME_DATABASE);
            FirebaseRemoteConfig                 = new Bridge("Firebase RemoteConfig","Firebase.RemoteConfig",BridgeSymbol.FIREBASE_REMOTE_CONFIG);
            UnityRemoteConfig                    = new Bridge("Unity Remote Config ","Unity.RemoteConfig",BridgeSymbol.UNITY_REMOTE_CONFIG);
            FirebaseStorage                      = new Bridge("Firebase Storage","Firebase.Storage",BridgeSymbol.FIREBASE_STORAGE);

            bridges = new List<Bridge>()
            {
                Admob                               ,
                UnityAds                            ,
                GoogleSignIn                        ,
                FacebookSignIn                      ,
                PlayServiceSignIn                   ,
                FirebaseSignIn                      ,
                TextureCache                        ,
                FirebaseRealtimeDatabase            ,
                UnityIAP                            ,
                Localize                            ,
                FirebaseRemoteConfigRealtimeDatabase,
                FirebaseRemoteConfig                ,
                UnityRemoteConfig                   ,
                FirebaseStorage                     ,
            };
        }
        #endregion
    
    }
}
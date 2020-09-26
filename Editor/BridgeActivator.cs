using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using UnityEditor;

namespace Evesoft.Editor
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
            window.Refresh();
            window.Show();
        }
        #endregion

        #region Field

        [ShowInInspector,HideLabel]
        private BridgeGroup Ads = new BridgeGroup(nameof(Ads),new Bridge[]
        {
            new Bridge("Admob",DefineSymbol.ADMOB),
            new Bridge("UnityAds",DefineSymbol.UNITY_ADS)
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Auth = new BridgeGroup(nameof(Auth),new Bridge[]
        {
            new Bridge("Google",DefineSymbol.GOOGLE_AUTH),
            new Bridge("Facebook",DefineSymbol.FACEBOOK_AUTH),
            new Bridge("Play Service",DefineSymbol.PLAYSERVICE_AUTH),
            new Bridge("Firebase",DefineSymbol.FIREBASE_AUTH)
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Cache = new BridgeGroup(nameof(Cache),new Bridge[]
        {
            new Bridge("Texture2D",DefineSymbol.CACHE_TEXTURE2D),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Database = new BridgeGroup(nameof(Database),new Bridge[]
        {
            new Bridge("Firebase",DefineSymbol.FIREBASE_REALTIME_DATABASE),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup IAP = new BridgeGroup("In App Purchase",new Bridge[]
        {
            new Bridge("Unity IAP",DefineSymbol.UNITY_IAP),
        });
     
        [ShowInInspector,HideLabel]
        private BridgeGroup localize = new BridgeGroup(nameof(localize),new Bridge[]
        {
            new Bridge("Localize",DefineSymbol.LOCALIZE),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup RemoteConfig = new BridgeGroup(nameof(RemoteConfig),new Bridge[]
        {
            new Bridge("Firebase Remote Config",DefineSymbol.FIREBASE_REMOTE_CONFIG),
            new Bridge("Unity Remote Config",DefineSymbol.UNITY_REMOTE_CONFIG),
        });

        [ShowInInspector,HideLabel]
        private BridgeGroup Storage = new BridgeGroup(nameof(Storage),new Bridge[]
        {
            new Bridge("Firebase Storage",DefineSymbol.FIREBASE_STORAGE),
        });
        #endregion
    
        public void Refresh()
        {
            Ads.Refresh();
            Auth.Refresh();
            Cache.Refresh();
            Database.Refresh();
            IAP.Refresh();
            localize.Refresh();
            RemoteConfig.Refresh();
            Storage.Refresh();
        }
    }
}
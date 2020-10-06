#if ODIN_INSPECTOR 
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Evesoft.Editor.ScriptingDefineSymbol;
using UnityEditor;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker]
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
        FirebaseStorage,
        Share ,
        YarnSpinner;
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

            foreach (var bridge in bridges)
                bridge?.RefreshRequired();
        }
        public void OnWindowClicked()
        {
            //Refresh();
        }
        public void OnGUI()
        {
           
        }
        #endregion

        private bool ShowApplyOrCancelBtn()
        {
            if(EditorApplication.isCompiling)
                return false;

            var symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
           
            if(!bridges.IsNullOrEmpty())
            {
                foreach (var bridge in bridges)
                {
                    if(bridge.isEnable && !symbols.Contains(bridge.symbol))
                        return true;
                    
                    if(!bridge.isEnable && symbols.Contains(bridge.symbol))
                        return true;
                }
            }

            return false;
        }

        [Button,PropertyOrder(-1),HorizontalGroup,ShowIf(nameof(ShowApplyOrCancelBtn))]
        public void Apply()
        {
            var symbols = ScriptingDefineSymbolUtility.GetDefineSymbol();
            var pluginNotInstalled = default(IList<Bridge>);

            foreach (var bridge in bridges)
            {
                if(!bridge.symbol.IsNullOrEmpty())
                {
                    if(bridge.isEnable)
                    {
                        if(!bridge.isInstalled)
                        {
                            if(pluginNotInstalled.IsNull())
                                pluginNotInstalled = new List<Bridge>();

                            pluginNotInstalled.Add(bridge);
                        }
                        else if(!symbols.Contains(bridge.symbol))
                        {
                            symbols.Add(bridge.symbol); 
                        }
                    }      
                    else if(!bridge.isEnable)
                    {
                        symbols.Remove(bridge.symbol); 
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

        [Button,PropertyOrder(-1),HorizontalGroup,ShowIf(nameof(ShowApplyOrCancelBtn))]
        public void Cancel()
        {
            Refresh();
        }

        #region constructor
        public BridgeEditor()
        {
            Admob                                = new Bridge("Admob",BridgeSymbol.ADMOB,new Reference.AdmobConfigReference(),new Prequested("Admob","GoogleMobileAds.Api","https://github.com/googleads/googleads-mobile-unity/releases",BuildTarget.Android,BuildTarget.iOS));
            UnityAds                             = new Bridge("Unity Ads",BridgeSymbol.UNITY_ADS,new Reference.UnityAdsConfigReference(),new Prequested("Unity Ads","UnityEngine.Advertisements.IUnityAdsListener,UnityEngine.Advertisements","https://docs.unity3d.com/Packages/com.unity.ads@3.4/manual/MonetizationContentTypes.html",BuildTarget.Android,BuildTarget.iOS));
            GoogleSignIn                         = new Bridge("Google Sign In",BridgeSymbol.GOOGLE_AUTH,new Reference.GoogleSignInConfigReference(),new Prequested("Google Sign In","Google","https://github.com/googlesamples/google-signin-unity",BuildTarget.Android,BuildTarget.iOS));
            FacebookSignIn                       = new Bridge("Facebook",BridgeSymbol.FACEBOOK_AUTH,null,new Prequested("Facebook","Facebook.Unity","https://developers.facebook.com/docs/unity/",BuildTarget.Android,BuildTarget.iOS));
            PlayServiceSignIn                    = new Bridge("Play Service",BridgeSymbol.PLAYSERVICE_AUTH,null,new Prequested("Play Service","GooglePlayGames","https://github.com/playgameservices/play-games-plugin-for-unity",BuildTarget.Android,BuildTarget.iOS));
            FirebaseSignIn                       = new Bridge("Firebase Auth",BridgeSymbol.FIREBASE_AUTH,null,new Prequested("Firebase Auth","Firebase.Auth","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS));
            TextureCache                         = new Bridge("Cache - Texture2D",BridgeSymbol.CACHE_TEXTURE2D,new Reference.TextureCacheReference());
            FirebaseRealtimeDatabase             = new Bridge("Firebase Database",BridgeSymbol.FIREBASE_REALTIME_DATABASE,null,new Prequested("Firebase Database","Firebase.Database","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS));
            UnityIAP                             = new Bridge("Unity IAP",BridgeSymbol.UNITY_IAP,null,new Prequested("Unity IAP","UnityEngine.Purchasing","https://docs.unity3d.com/Manual/UnityIAP.html",BuildTarget.Android,BuildTarget.iOS,BuildTarget.StandaloneWindows,BuildTarget.StandaloneOSX));
            Localize                             = new Bridge("Localize",BridgeSymbol.LOCALIZE,new Reference.LocalizeReference());
            FirebaseRemoteConfigRealtimeDatabase = new Bridge("Firebase RemoteConfig with FirebaseDatabase",BridgeSymbol.FIREBASE_REALTIME_DATABASE,null,new Prequested("Firebase Database","Firebase.Database","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS));
            FirebaseRemoteConfig                 = new Bridge("Firebase RemoteConfig",BridgeSymbol.FIREBASE_REMOTE_CONFIG,null,new Prequested("Firebase RemoteConfig","Firebase.RemoteConfig","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS));
            UnityRemoteConfig                    = new Bridge("Unity Remote Config",BridgeSymbol.UNITY_REMOTE_CONFIG,null,new Prequested("Unity Remote Config","Unity.RemoteConfig","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS,BuildTarget.StandaloneWindows,BuildTarget.StandaloneOSX));
            FirebaseStorage                      = new Bridge("Firebase Storage",BridgeSymbol.FIREBASE_STORAGE,null,new Prequested("Firebase Storage","Firebase.Storage","https://firebase.google.com/docs/unity/setup",BuildTarget.Android,BuildTarget.iOS));
            Share                                = new Bridge("Share",BridgeSymbol.NATIVE_SHARE,null,new Prequested("Native Share","NativeShare,NativeShare.Runtime","https://assetstore.unity.com/packages/tools/integration/native-share-for-android-ios-112731",BuildTarget.Android,BuildTarget.iOS));
            YarnSpinner                          = new Bridge("YarnSpinner",BridgeSymbol.YARN_SPINNER,new Reference.YarnSpinnerReference(),new Prequested("Yarn Spinner","Yarn.Unity","https://yarnspinner.dev/"));

            var guide = @"using Evesoft.Ads;
using Sirenix.OdinInspector;

public class Test : SerializedMonoBehaviour
{
    public iAdsConfig config;

    private void Start()
    {
        var service = AdsServiceFactory.CreateService(config);
            service.onRewardedVideo += OnRewaredVideo;
            service.ShowRewardVideo();
    }

    private void OnRewaredVideo(iAdsService service)
    {
        //DOStuff
    }
}";

            Admob.AddHowToUse(guide);
            UnityAds.AddHowToUse(guide);

            GoogleSignIn.AddHowToUse(@"using Evesoft.CloudService;
using Sirenix.OdinInspector;

public class Test : SerializedMonoBehaviour
{
    private void Start()
    {
        var config  = CloudAuthConfigFactory.CreateGoogleAuthConfig(Your webClientID); 
        var service = CloudAuthFactory.CreateAuth(config);
            service.Login();
    }
}");
            FacebookSignIn.AddHowToUse(@"using Evesoft.CloudService;
using Sirenix.OdinInspector;

public class Test : SerializedMonoBehaviour
{
    public iCloudAuthConfig config;

    private void Start()
    {
        var config  = CloudAuthConfigFactory.CreateFacebookAuthConfig(); 
        var service = CloudAuthFactory.CreateAuth(config);
            service.Login();
    }
}");
            PlayServiceSignIn.AddHowToUse(@"using Evesoft.CloudService;
using Sirenix.OdinInspector;

public class Test : SerializedMonoBehaviour
{
    public iCloudAuthConfig config;

    private void Start()
    {
        var config  = CloudAuthConfigFactory.CreatePlayServiceAuthConfig(); 
        var service = CloudAuthFactory.CreateAuth(config);
            service.Login();
    }
}");
            FirebaseSignIn.AddHowToUse(@"using Evesoft.CloudService;
using Sirenix.OdinInspector;

public class Test : SerializedMonoBehaviour
{
    private iCloudAuth service;

    private void Start()
    {
        var config  = CloudAuthConfigFactory.CreateFirebaseAuthConfig();
            service = CloudAuthFactory.CreateAuth(config);
    }

    private void LoginWithEmailPassword(string email,string password)
    {
        var options = CloudAuthOptionsFactory.CreateFirebaseWithEmailPassword(email,password);
        service.Login(options);
    }

    private void LoginWithGooogle(string token)
    {
        var options = CloudAuthOptionsFactory.CreateFirebaseWithGoogle(token);
        service.Login(options);
    }

    private void LoginWithPlayService(string token)
    {
        var options = CloudAuthOptionsFactory.CreateFirebaseWithGooglePlayService(token);
        service.Login(options);
    }

    private void LoginWithFacebook(string token)
    {
        var options = CloudAuthOptionsFactory.CreateFirebaseWithFacebook(token);
        service.Login(options);
    }
}
");
            TextureCache.AddHowToUse(@"
using UnityEngine;
using Evesoft;
using Evesoft.Cache;

public class Test : MonoBehaviour
{
    public Texture2D texture;
    public string key;
    public string url;

    private void Start()
    {
        var cache = Texture2DCache.defaultInstance.GetCache(url,key);
        if(cache.IsNull())
            Texture2DCache.defaultInstance.AddCache(new Texture2DCacheData(url,key,texture));
    }
}");
            Localize.AddHowToUse(@"1. Create Database     : RightClick On Project Window -> Create -> Evesoft -> Localize -> LocalizeDatabase.
2. Create LocalizeData : RightClick On Project Window -> Create -> Evesoft -> Localize -> LocalizeData.
3. Add Localize Component to Any GameObject you want localize, then choose localize data you want to use by dropdown.
4. To change language just change language on Database already created");
            FirebaseRealtimeDatabase.AddHowToUse(@"
using UnityEngine;
using Evesoft;
using Evesoft.CloudService;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        var rootCoin = ""Coins"";
        var config   = CloudDatabaseConfigFactory.CreateFirebaseDatabaseConfig();
        var service  = CloudDatabaseFactory.CreateDatabase(config);
        var options  = CloudDatabaseOptionsFactory.CreateFirebaseDatabaseOptions(rootCoin);

        (var reference,var exception) = await service.Connect(options);

        if(!exception.IsNull())
        {
            exception.Message.LogError();
            return;
        }

        reference.data.Log();
        reference.events.onDataAdded += OnDataAdded;
    }

    private void OnDataAdded(string key, object value)
    {
        //Do stuff
    }
}");
            FirebaseRemoteConfigRealtimeDatabase.AddHowToUse(@"
using UnityEngine;
using Evesoft;
using Evesoft.CloudService;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        var versionKey   = ""version"";
        var versionValue = default(string);
        var devMode = true;
        var config  = CloudRemoteSettingFactory.CreateFirebaseRemoteSetting(Evesoft.CloudService.Firebase.FirebaseCloudRemoteConfigType.RealtimeDatabase,devMode);
        var service = CloudRemoteConfigFactory.CreateRemoteConfig(config);
        await service.Fetch();
        versionValue = service.GetConfig<string>(versionKey);
        versionValue.Log();
    }
}");
            FirebaseRemoteConfig.AddHowToUse(@"
using UnityEngine;
using Evesoft;
using Evesoft.CloudService;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        var versionKey   = ""version"";
        var versionValue = default(string);
        var devMode = true;
        var config  = CloudRemoteSettingFactory.CreateFirebaseRemoteSetting(Evesoft.CloudService.Firebase.FirebaseCloudRemoteConfigType.RemoteConfig,devMode);
        var service = CloudRemoteConfigFactory.CreateRemoteConfig(config);
        await service.Fetch();
        versionValue = service.GetConfig<string>(versionKey);
        versionValue.Log();
    }
}");
            UnityRemoteConfig.AddHowToUse(@"
using UnityEngine;
using Evesoft;
using Evesoft.CloudService;

public class Test : MonoBehaviour
{
    public struct UserAttribute
    {
        public string id;
        public string score;
        public string level;
    }
    public struct AppAtribute
    {
        public string version;
        public string bundleVersion;
    }

    private async void Start()
    {
        var versionKey   = ""version"";
        var versionValue = default(string);
        var config  = CloudRemoteSettingFactory.CreateUnityRemoteSetting(new UserAttribute(),new AppAtribute());
        var service = CloudRemoteConfigFactory.CreateRemoteConfig(config);
        await service.Fetch();
        versionValue = service.GetConfig<string>(versionKey);
        versionValue.Log();
    }
}");
            FirebaseStorage.AddHowToUse(@"
using UnityEngine;
using Evesoft.CloudService;

public class Test : MonoBehaviour
{
    private async void Start()
    {
        var path    = ""your image path"";
        var config  = CloudStorageConfigFactory.CreateFirebaseStorageConfig();
        var service = CloudStorageFactory.CreateStorage(config);
        (var Texture,var exception) = await service.DownloadTexture(path);
    }
}");
            Share.AddHowToUse(@"
using UnityEngine;
using Evesoft.Share;

public class Test : MonoBehaviour
{
    private NativeShare share;

    private void Start()
    {
        var share = ShareFactory.CreateShare(ShareType.NativeShare);
        share.SetText(""Hello World"").SetSubject(""mySubject"").Share();
    }
}");

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
                Share,
                YarnSpinner,
            };
        }
        #endregion
    }
}
#endif
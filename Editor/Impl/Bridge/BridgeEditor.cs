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

            var guide = @"Create config first. RightClick On Project -> Evesoft -> Ads

using <color=#E5C07B>Evesoft.Ads</color>;
using <color=#E5C07B>Sirenix.OdinInspector</color>;

public class <color=#E5C07B>Test : SerializedMonoBehaviour</color>
{
    public <color=#E5C07B>iAdsConfig</color> config;

    private void <color=#59AFD2>Start</color>()
    {
        var service = AdsServiceFactory.<color=#59AFD2>CreateService</color>(config);
        service.onRewardedVideo += OnRewaredVideo;
        service.<color=#59AFD2>ShowRewardVideo</color>();
    }

    private void <color=#59AFD2>OnRewaredVideo</color>(<color=#E5C07B>iAdsService</color> service)
    {
        //DOStuff
    }
}";
            guide = ColoringScript(guide);
            Admob.AddHowToUse(guide);
            UnityAds.AddHowToUse(guide);

            guide = @"<color=#E5C07B>using Evesoft.CloudService</color>;
using <color=#E5C07B>Sirenix.OdinInspector</color>;

public class <color=#E5C07B>Test : SerializedMonoBehaviour</color>
{
    private void <color=#59AFD2>Start</color>()
    {
        var config  = CloudAuthConfigFactory.<color=#59AFD2>CreateGoogleAuthConfig</color>(Your webClientID); 
        var service = CloudAuthFactory.<color=#59AFD2>CreateAuth</color>(config);
        service.<color=#59AFD2>Login</color>();
    }
}";
            guide = ColoringScript(guide);
            GoogleSignIn.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>Evesoft.CloudService</color>;
using <color=#E5C07B>Sirenix.OdinInspector</color>;

public class <color=#E5C07B>Test : SerializedMonoBehaviour</color>
{
    public <color=#E5C07B>iCloudAuthConfig</color> config;

    private void <color=#59AFD2>Start</color>()
    {
        var config  = CloudAuthConfigFactory.<color=#59AFD2>CreateFacebookAuthConfig</color>(); 
        var service = CloudAuthFactory.<color=#59AFD2>CreateAuth</color>(config);
        service.<color=#59AFD2>Login</color>();
    }
}";
            guide = ColoringScript(guide);
            FacebookSignIn.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>Evesoft.CloudService</color>;
using <color=#E5C07B>Sirenix.OdinInspector</color>;

public <color=#E5C07B>class Test : SerializedMonoBehaviour</color>
{
    public <color=#E5C07B>iCloudAuthConfig</color> config;

    private void <color=#59AFD2>Start</color>()
    {
        var config  = CloudAuthConfigFactory.<color=#59AFD2>CreatePlayServiceAuthConfig</color>(); 
        var service = CloudAuthFactory.<color=#59AFD2>CreateAuth</color>(config);
            service.<color=#59AFD2>Login</color>();
    }
}";
            guide = ColoringScript(guide);
            PlayServiceSignIn.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>Evesoft.CloudService</color>;
using <color=#E5C07B>Sirenix.OdinInspector</color>;

public class <color=#E5C07B>Test : SerializedMonoBehaviour</color>
{
    private <color=#E5C07B>iCloudAuth</color> service;

    private void <color=#59AFD2>Start</color>()
    {
        var config  = CloudAuthConfigFactory.<color=#59AFD2>CreateFirebaseAuthConfig</color>();
        service = CloudAuthFactory.<color=#59AFD2>CreateAuth</color>(config);
    }

    private void <color=#59AFD2>LoginWithEmailPassword</color>(string email,string password)
    {
        var options = CloudAuthOptionsFactory.<color=#59AFD2>CreateFirebaseWithEmailPassword</color>(email,password);
        service.<color=#59AFD2>Login</color>(options);
    }

    private void <color=#59AFD2>LoginWithGooogle</color>(string token)
    {
        var options = CloudAuthOptionsFactory.<color=#59AFD2>CreateFirebaseWithGoogle</color>(token);
        service.<color=#59AFD2>Login</color>(options);
    }

    private void <color=#59AFD2>LoginWithPlayService</color>(string token)
    {
        var options = CloudAuthOptionsFactory.<color=#59AFD2>CreateFirebaseWithGooglePlayService</color>(token);
        service.<color=#59AFD2>Login</color>(options);
    }

    private void <color=#59AFD2>LoginWithFacebook</color>(string token)
    {
        var options = CloudAuthOptionsFactory.<color=#59AFD2>CreateFirebaseWithFacebook</color>(token);
        service.<color=#59AFD2>Login</color>(options);
    }
}";
            guide = ColoringScript(guide);
            FirebaseSignIn.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft</color>;
using <color=#E5C07B>Evesoft.Cache</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    public <color=#E5C07B>Texture2D</color> texture;
    public string key;
    public string url;

    private void <color=#59AFD2>Start</color>()
    {
        var cache = Texture2DCache.defaultInstance.<color=#59AFD2>GetCache</color>(url,key);
        if(cache.<color=#59AFD2>IsNull</color>())
            Texture2DCache.defaultInstance.<color=#59AFD2>AddCache</color>(new Texture2DCacheData(url,key,texture));
    }
}";
            guide = ColoringScript(guide);
            TextureCache.AddHowToUse(guide);
            
            guide = @"1. Create Database     : RightClick On Project Window -> Create -> Evesoft -> Localize -> LocalizeDatabase.
2. Create LocalizeData : RightClick On Project Window -> Create -> Evesoft -> Localize -> LocalizeData.
3. Add Localize Component to Any GameObject you want localize, then choose localize data you want to use by dropdown.
4. To change language just change language on Database already created";
            guide = ColoringScript(guide);
            Localize.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft</color>;
using <color=#E5C07B>Evesoft.CloudService</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    private async void <color=#59AFD2>Start</color>()
    {
        var rootCoin = <color=#98C36E>""Coins""</color>;
        var config   = CloudDatabaseConfigFactory.<color=#59AFD2>CreateFirebaseDatabaseConfig</color>();
        var service  = CloudDatabaseFactory.<color=#59AFD2>CreateDatabase</color>(config);
        var options  = CloudDatabaseOptionsFactory.<color=#59AFD2>CreateFirebaseDatabaseOptions</color>(rootCoin);

        (var reference,var exception) = await service.<color=#59AFD2>Connect</color>(options);

        if(!exception.<color=#59AFD2>IsNull</color>())
        {
            exception.Message.<color=#59AFD2>LogError</color>();
            return;
        }

        reference.data.<color=#59AFD2>Log</color>();
        reference.events.onDataAdded += OnDataAdded;
    }

    private void <color=#59AFD2>OnDataAdded</color>(string key, object value)
    {
        //Do stuff
    }
}";
            guide = ColoringScript(guide);
            FirebaseRealtimeDatabase.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft</color>;
using <color=#E5C07B>Evesoft.CloudService</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    private async void <color=#59AFD2>Start</color>()
    {
        var versionKey   = <color=#98C36E>""version""</color>;
        var versionValue = default(string);
        var devMode = true;
        var config  = CloudRemoteSettingFactory.<color=#59AFD2>CreateFirebaseRemoteSetting</color>(
            Evesoft.CloudService.Firebase.FirebaseCloudRemoteConfigType.RealtimeDatabase,
            devMode);
        var service = CloudRemoteConfigFactory.<color=#59AFD2>CreateRemoteConfig</color>(config);
        await service.<color=#59AFD2>Fetch</color>();
        versionValue = service.<color=#59AFD2>GetConfig</color><string>(versionKey);
        versionValue.<color=#59AFD2>Log</color>();
    }
}";
            guide = ColoringScript(guide);
            FirebaseRemoteConfigRealtimeDatabase.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft</color>;
using <color=#E5C07B>Evesoft.CloudService</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    private async void <color=#59AFD2>Start</color>()
    {
        var versionKey   = <color=#98C36E>""version""</color>;
        var versionValue = default(string);
        var devMode = true;
        var config  = CloudRemoteSettingFactory.<color=#59AFD2>CreateFirebaseRemoteSetting</color>(
            Evesoft.CloudService.Firebase.FirebaseCloudRemoteConfigType.RemoteConfig,
            devMode);
        var service = CloudRemoteConfigFactory.<color=#59AFD2>CreateRemoteConfig</color>(config);
        await service.<color=#59AFD2>Fetch</color>();
        versionValue = service.<color=#59AFD2>GetConfig</color><string>(versionKey);
        versionValue.<color=#59AFD2>Log</color>();
    }
}";
            guide = ColoringScript(guide);
            FirebaseRemoteConfig.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft</color>;
using <color=#E5C07B>Evesoft.CloudService</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    public struct <color=#E5C07B>UserAttribute</color>
    {
        public string id;
        public string score;
        public string level;
    }
    public struct <color=#E5C07B>AppAtribute</color>
    {
        public string version;
        public string bundleVersion;
    }

    private async void <color=#59AFD2>Start</color>()
    {
        var versionKey   = <color=#98C36E>""version""</color>;
        var versionValue = default(string);
        var config  = CloudRemoteSettingFactory.<color=#59AFD2>CreateUnityRemoteSetting</color>(new <color=#E5C07B>UserAttribute</color>(),new <color=#E5C07B>AppAtribute</color>());
        var service = CloudRemoteConfigFactory.<color=#59AFD2>CreateRemoteConfig</color>(config);
        await service.<color=#59AFD2>Fetch</color>();
        versionValue = service.<color=#59AFD2>GetConfig</color><string>(versionKey);
        versionValue.<color=#59AFD2>Log</color>();
    }
}";
            guide = ColoringScript(guide);
            UnityRemoteConfig.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft.CloudService</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    private async void <color=#59AFD2>Start</color>()
    {
        var path    = <color=#98C36E>""your image path""</color>;
        var config  = CloudStorageConfigFactory.<color=#59AFD2>CreateFirebaseStorageConfig</color>();
        var service = CloudStorageFactory.<color=#59AFD2>CreateStorage</color>(config);
        (var Texture,var exception) = await service.<color=#59AFD2>DownloadTexture</color>(path);
    }
}";
            guide = ColoringScript(guide);
            FirebaseStorage.AddHowToUse(guide);
            
            guide = @"using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft.Share</color>;

public class <color=#E5C07B>Test : MonoBehaviour</color>
{
    private <color=#E5C07B>NativeShare</color> share;

    private void <color=#59AFD2>Start</color>()
    {
        var share = ShareFactory.<color=#59AFD2>CreateShare</color>(ShareType.NativeShare);
        share.<color=#59AFD2>SetText</color>(<color=#98C36E>""Hello World""</color>).<color=#59AFD2>SetSubject</color>(<color=#98C36E>""mySubject""</color>).<color=#59AFD2>Share</color>();
    }
}";
            guide = ColoringScript(guide);
            Share.AddHowToUse(guide);
            
            guide = @"
//========== UIDialog.cs =========//
using <color=#E5C07B>UnityEngine</color>;
using <color=#E5C07B>Evesoft.Dialogue</color>;
using <color=#E5C07B>System.Collections.Generic</color>;

public class <color=#E5C07B>UIDialog : MonoBehaviour, IDialogueUI</color>
{
    public void <color=#59AFD2>OnDialogueStart</color>()
    {
       //your implementation
    }
    public void <color=#59AFD2>OnDialogueEnd</color>()
    {
       //your implementation
    }
    public void <color=#59AFD2>OnLineStart</color>(<color=#E5C07B>IDialogueLine</color> line)
    {
       //your implementation
    }
    public void <color=#59AFD2>OnLineEnd</color>()
    {
        //your implementation
    }
    public void <color=#59AFD2>OnOptionsStart</color>(<color=#E5C07B>IList</color><<color=#E5C07B>IDialogueOptions</color>> options)
    {
        //your implementation
    }
    public void <color=#59AFD2>OnOptionsSelected</color>(int ID)
    {
        //your implementation
    }
    public void <color=#59AFD2>OnOptionsEnd</color>()
    {
       //your implementation
    }  
    public void <color=#59AFD2>OnCommand</color>(string command)
    {
       //your implementation
    }

    public void <color=#59AFD2>Dispose</color>()
    {
        //your implementation
    }
}

//========== DialogueManager.cs =========//
public class <color=#E5C07B>DialogueManager : SerializedMonoBehaviour</color>
{
    #region field
    <color=#E5C07B>[OdinSerialize,ShowInInspector]</color>
    private <color=#E5C07B>iDialogueUI</color> _UI;

    <color=#E5C07B>[SerializeField,ShowInInspector]</color>
    private <color=#E5C07B>YarnProgram</color>[] dialogue;

    <color=#E5C07B>[OdinSerialize,ShowInInspector]</color>
    private <color=#E5C07B>IDictionary</color><string,object> _defaultVariables;
    #endregion

    #region private
    private <color=#E5C07B>iDialogue</color> _dialogue;
    #endregion

    #region methods
    private void <color=#59AFD2>Start</color>()
    {
        var config   = DialogueConfigFactory.<color=#59AFD2>CreateYarnSpinnerConfig</color>(_UI,_defaultVariables,dialogue);
        _dialogue    = DialogueFactory.<color=#59AFD2>Create</color>(config);
        _dialogue.<color=#59AFD2>StartDialogue</color>(<color=#98C36E>""Sally""</color>);
    }
    #endregion
}";
            guide = ColoringScript(guide);
            YarnSpinner.AddHowToUse(guide);

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

        private string ColoringScript(string text)
        {
            var color1 = "#BC78DD";
            text = text.Replace("using ",$"<color={color1}>using </color>");
            text = text.Replace("class ",$"<color={color1}>class </color>");
            text = text.Replace("struct ",$"<color={color1}>struct </color>");
            text = text.Replace("public ",$"<color={color1}>public </color>");
            text = text.Replace("private ",$"<color={color1}>private </color>");
            text = text.Replace("internal ",$"<color={color1}>internal </color>");
            text = text.Replace("var ",$"<color={color1}>var </color>");
            text = text.Replace("void ",$"<color={color1}>void </color>");
            text = text.Replace("string",$"<color={color1}>string</color>");
            text = text.Replace("int ",$"<color={color1}>int </color>");
            text = text.Replace("float ",$"<color={color1}>float </color>");
            text = text.Replace("double ",$"<color={color1}>double </color>");
            text = text.Replace("new ",$"<color={color1}>new </color>");
            text = text.Replace("return ",$"<color={color1}>return </color>");
            text = text.Replace("await ",$"<color={color1}>await </color>");
            text = text.Replace("async ",$"<color={color1}>async </color>");
            text = text.Replace("object ",$"<color={color1}>object </color>");
            text = text.Replace("default ",$"<color={color1}>default </color>");
            text = text.Replace("default(",$"<color={color1}>default</color>(");
            
            return text;
        }
    }
}
#endif
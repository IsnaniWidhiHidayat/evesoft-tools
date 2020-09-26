namespace  Evesoft.CloudService
{
    public static class CloudRemoteSettingFactory
    {
        public static iCloudRemoteSetting CreateUnityRemoteSetting<T1,T2>(T1 userAttributs,T2 appAtributs,string userCustomID = null) where T1:struct where T2:struct
        {
            var result = new UnityRemoteConfig.UnityRemoteSetting(userCustomID);
                result.SetUserAttribute(userAttributs);
                result.SetAppAttribute(appAtributs);
            return result;
        }
        public static iCloudRemoteSetting CreateFirebaseRemoteSetting(Firebase.FirebaseCloudRemoteConfigType type,bool devMode)
        {
            return new Firebase.FirebaseCloudRemoteSetting(type,devMode);
        }
    }
}
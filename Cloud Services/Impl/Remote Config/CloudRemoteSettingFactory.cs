namespace  Evesoft.CloudService
{
    public static class CloudRemoteSettingFactory
    {
        public static iCloudRemoteSetting CreateUnityRemoteSetting<T1,T2>(T1 userAttributs,T2 appAtributs) where T1:struct where T2:struct
        {
            var result = new UnityRemoteConfig.UnityRemoteSetting();
                result.SetUserAttribute(userAttributs);
                result.SetAppAttribute(appAtributs);
            return result;
        }
        public static iCloudRemoteSetting CreateFirebaseRemoteSetting(Firebase.FirebaseCloudRemoteConfigType type)
        {
            return new Firebase.FirebaseCloudRemoteSetting(type);
        }
    }
}
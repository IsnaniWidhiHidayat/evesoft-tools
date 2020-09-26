using System;
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudRemoteConfigFactory
    {
        private static IDictionary<CloudRemoteConfigType,object> remoteConfigs = new Dictionary<CloudRemoteConfigType,object>();

        public static iCloudRemoteConfig CreateRemoteConfig(iCloudRemoteSetting setting)
        {
            if(setting.IsNull())
                return null;

            var service = setting.GetConfig<CloudRemoteConfigType>(nameof(CloudService));
            
            switch(service)
            {
                #if UNITY_REMOTE_CONFIG
                case CloudRemoteConfigType.UnityRemoteConfig:
                {
                    if(remoteConfigs.ContainsKey(service))
                        return remoteConfigs[service] as iCloudRemoteConfig;

                    var userAttribute = setting.GetConfig<object>(UnityRemoteConfig.UnityRemoteSetting.USER);
                    var appAttribute  = setting.GetConfig<object>(UnityRemoteConfig.UnityRemoteSetting.APP);
                    var userCustomID  = setting.GetConfig<string>(UnityRemoteConfig.UnityRemoteSetting.USERID);

                    var type = typeof(UnityRemoteConfig.UnityRemoteConfig<,>).MakeGenericType(userAttribute.GetType(),appAttribute.GetType());
                    var a_Context = Activator.CreateInstance(type,userAttribute,appAttribute,userCustomID);  

                    var config = default(iCloudRemoteConfig);
                    Activator.CreateInstance(type).To<iCloudRemoteConfig>(out config);
                    remoteConfigs[service] = config;
                    return config;
                }
                #endif

                #if FIREBASE_REMOTE_CONFIG
                case CloudRemoteConfigType.FirebaseRemoteConfig:
                {
                    var type            = setting.GetConfig<Firebase.FirebaseCloudRemoteConfigType>(Firebase.FirebaseCloudRemoteSetting.TYPE);
                    var firebaseConfigs = default(IDictionary<Firebase.FirebaseCloudRemoteConfigType,iCloudRemoteConfig>);

                    //Add service
                    if(!remoteConfigs.ContainsKey(service))
                        remoteConfigs[service] = new Dictionary<Firebase.FirebaseCloudRemoteConfigType,iCloudRemoteConfig>();
                         
                    firebaseConfigs = remoteConfigs[service] as IDictionary<Firebase.FirebaseCloudRemoteConfigType,iCloudRemoteConfig>;
                    if(firebaseConfigs.ContainsKey(type))
                        return firebaseConfigs[type];
    
                    return firebaseConfigs[type] = new Firebase.FirebaseCloudRemoteConfig(setting);
                }
                #endif

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
    }
}
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using FRC = Firebase.RemoteConfig.FirebaseRemoteConfig;
using Sirenix.OdinInspector;
using Firebase.RemoteConfig;

namespace Evesoft.CloudService.Firebase
{
    [HideReferenceObjectPicker]
    public class FirebaseCloudRemoteConfig : iCloudRemoteConfig
    {
        #region private
        private IDictionary<string, object> _configs;
        private FirebaseCloudRemoteConfigType _type;
        private iCloudDatabaseReference _reference;
        private bool _fetched;
        private bool _fetching;
        #endregion

        [ShowInInspector,DisplayAsString]
        private ICollection<string> _keys => _configs?.Keys;
        
        #region iCloudRemoteConfig
        public bool isfetched => _fetched;
        public bool isHaveConfigs => !FRC.Keys.IsNullOrEmpty();
        public T GetConfig<T>(string key)
        {
            switch(_type)
            {
                case FirebaseCloudRemoteConfigType.RealtimeDatabase:
                {
                    if(_reference.data.IsNull())
                        break;

                    var result = default(T);
                    if(!_reference.data.IsNullOrEmpty() && _reference.data.ContainsKey(key))
                        if(_reference.data[key].To<T>(out result))
                            return result;

                    break;
                }    

                case FirebaseCloudRemoteConfigType.RemoteConfig:
                {
                    var cfg = FRC.GetValue(key);
                    var result = default(T);
       
                    //interger
                    if(typeof(T) == typeof(int) && FRC.GetValue(key).DoubleValue.ToInt32().To<T>(out result))
                        return result;
                    
                    //Float
                    if(typeof(T) == typeof(float) && FRC.GetValue(key).DoubleValue.ToSingle().To<T>(out result))
                        return result;

                    //double
                    if(typeof(T) == typeof(double) && FRC.GetValue(key).DoubleValue.To<T>(out result))
                        return result;

                    //Bool
                    if(typeof(T) == typeof(bool) && FRC.GetValue(key).BooleanValue.To<T>(out result))
                        return result;
                
                    //Long
                    if(typeof(T) == typeof(long) && FRC.GetValue(key).LongValue.To<T>(out result))
                        return result;

                    //Long
                    if(typeof(T) == typeof(string) && FRC.GetValue(key).StringValue.To<T>(out result))
                        return result;

                    break;
                }
            }

            return default(T);
        }       
        public async Task Fetch()
        {
            _fetched = false;

            switch(_type)
            {
                case FirebaseCloudRemoteConfigType.RealtimeDatabase:
                {
                    var database = CloudDatabaseFactory.CreateDatabase(CloudDatabaseConfigFactory.CreateFirebaseDatabaseConfig());
                    var exception = default(Exception);
                    (_reference,exception)  = await database.Connect(CloudDatabaseOptionsFactory.CreateFirebaseDatabaseOptions("config"));
                    
                    if(exception.IsNull())
                        _fetched = true;
                    
                    break;
                }

                case FirebaseCloudRemoteConfigType.RemoteConfig:
                {   
                    if(_fetching)
                        return;

                    _fetching = true;

                    await FRC.FetchAsync(TimeSpan.Zero);
                    FRC.ActivateFetched();

                    _fetched  = true;
                    _fetching = false;

                    break;
                }
            }
        }
        #endregion

        #region constructor
        public FirebaseCloudRemoteConfig(iCloudRemoteSetting setting)
        {
            _type = setting.GetConfig<FirebaseCloudRemoteConfigType>(nameof(FirebaseCloudRemoteConfigType));
            var devMode = setting.GetConfig<bool>(FirebaseCloudRemoteSetting.DEVMODE);

            _type.Log();
            devMode.Log();
              
            FirebaseRemoteConfig.Settings = new ConfigSettings()
            {
                IsDeveloperMode = devMode
            };    
        }
        #endregion
    }
}
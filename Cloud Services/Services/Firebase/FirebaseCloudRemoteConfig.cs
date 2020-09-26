using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using FRC = Firebase.RemoteConfig.FirebaseRemoteConfig;
using Sirenix.OdinInspector;
using Firebase.RemoteConfig;
using UnityEngine;

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
        private bool _devMode;
        #endregion

        [ShowInInspector,DisplayAsString]
        private ICollection<string> _keys => _configs?.Keys;
        
        #region iCloudRemoteConfig
        public bool isfetched => _fetched;
        public bool isHaveConfigs => !FRC.Keys.IsNullOrEmpty();
        public  T GetConfig<T>(string key)
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
                    await new WaitUntil(()=> _fetched);
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

        private async void Init(iCloudRemoteSetting setting)
        {
            _type    = setting.GetConfig<FirebaseCloudRemoteConfigType>(FirebaseCloudRemoteSetting.TYPE);
            _devMode = setting.GetConfig<bool>(FirebaseCloudRemoteSetting.DEVMODE);

            switch(_type){
                case FirebaseCloudRemoteConfigType.RealtimeDatabase:
                {
                    var database = CloudDatabaseFactory.CreateDatabase(CloudDatabaseConfigFactory.CreateFirebaseDatabaseConfig());
                    var exception = default(Exception);
                    var root = "config";
                    var dev  = "development";
                    var prod = "release";
                    var path = root;
                        path += string.Format("/{0}",_devMode? dev : prod) ;

                    #if UNITY_EDITOR
                    //Add Default data
                    (_reference,exception) = await database.Connect(CloudDatabaseOptionsFactory.CreateFirebaseDatabaseOptions(root));
                    
                    var isEmpty = _reference.IsNullOrEmpty();
                        isEmpty |= (!_reference.IsNullOrEmpty() && _reference.data.ContainsKey(root));
                    
                    if(isEmpty)
                    {
                        var data = new Dictionary<string,object>();
                        data[dev] = new Dictionary<string,object>
                        {
                            {"version",Application.version}
                        };
                        data[prod] = new Dictionary<string,object>
                        {
                            {"version",Application.version}
                        };

                        exception = await _reference.SetData(data);
                        _reference.Dispose();     
                    }
                    #endif

                    (_reference,exception)  = await database.Connect(CloudDatabaseOptionsFactory.CreateFirebaseDatabaseOptions(path));
                    
                    if(exception.IsNull())
                        _fetched = true;

                    break;
                }

                case FirebaseCloudRemoteConfigType.RemoteConfig:{
                    FirebaseRemoteConfig.Settings = new ConfigSettings()
                    {
                        IsDeveloperMode =_devMode
                    };  
                    break;
                }
            }
        }

        #region constructor
        public FirebaseCloudRemoteConfig(iCloudRemoteSetting setting)
        {
            Init(setting);
        }
        #endregion
    }
}
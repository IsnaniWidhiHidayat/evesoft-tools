using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using FRC = Firebase.RemoteConfig.FirebaseRemoteConfig;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.CloudService.Firebase
{
    public class FirebaseCloudRemoteConfig : iCloudRemoteConfig
    {
        #region private
        private IDictionary<string, object> _configs;
        private FirebaseCloudRemoteConfigType _type;
        private iCloudDatabaseReference _reference;
        private bool _fetched;
        #endregion
        
        #region iCloudRemoteConfig
        public bool isfetched => _fetched;
        public bool isHaveConfigs => throw new NotImplementedException();
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
                    await FRC.FetchAsync();
                    FRC.ActivateFetched();
                    _fetched = true;
                    break;
                }
            }
        }
        #endregion

        #region constructor
        public FirebaseCloudRemoteConfig(iCloudRemoteSetting setting)
        {
            _type = setting.GetConfig<FirebaseCloudRemoteConfigType>(nameof(FirebaseCloudRemoteConfigType));
        }
        #endregion
    }
}
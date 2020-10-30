#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.CloudService
{
    public static class CloudDatabaseFactory
    {
        public static bool IsNullOrEmpty(this ICloudDatabaseReference obj)
        {
            if(obj.IsNull())
                return true;

            if(obj.data.IsNullOrEmpty())
                return true;

            return false;
        }

        private static IDictionary<CloudDatabaseType,object> databases = new Dictionary<CloudDatabaseType,object>();

        public static ICloudDatabase Create(ICloudDatabaseConfig config)
        {
            if(config.IsNull())
                return null;

            var service = config.GetConfig<CloudDatabaseType>(nameof(CloudService));
            
            switch(service)
            {
                #if FIREBASE_REALTIME_DATABASE
                case CloudDatabaseType.FirebaseRealtimeDatabase:
                {
                    if(!databases.ContainsKey(service))
                        databases[service] = new Dictionary<string,ICloudDatabase>();
                        
                    var db = config.GetConfig<string>(Firebase.FirebaseCloudDatabaseConfig.DB);
                    if(db.IsNullOrEmpty())
                        db = "default";

                    var dic = databases[service] as Dictionary<string,ICloudDatabase>;
                    if(dic.ContainsKey(db))
                        return dic[db];

                    return dic[db] = new Firebase.FirebaseCloudDatabase(config);
                }
                #endif
                
                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
        public static ICloudDatabase Get(CloudDatabaseType type,params string[] param)
        {
            switch(type)
            {
                #if FIREBASE_REALTIME_DATABASE
                case CloudDatabaseType.FirebaseRealtimeDatabase:
                {
                    if(databases.ContainsKey(type))
                    {
                        var services = databases[type] as Dictionary<string,ICloudDatabase>;
                        var db       = param.IsNullOrEmpty()? "default" : param.First() ;
                        if(services.ContainsKey(db))
                            return services[db];
                    }

                    return null;  
                }
                #endif

                default:
                {
                    return null;
                }
            }
        }
        public static async Task<ICloudDatabase> GetAsync(CloudDatabaseType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
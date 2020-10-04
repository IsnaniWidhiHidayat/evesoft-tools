#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.CloudService
{
    public static class CloudDatabaseFactory
    {
        public static bool IsNullOrEmpty(this iCloudDatabaseReference obj)
        {
            if(obj.IsNull())
                return true;

            if(obj.data.IsNullOrEmpty())
                return true;

            return false;
        }

        private static IDictionary<CloudDatabaseType,object> databases = new Dictionary<CloudDatabaseType,object>();

        public static iCloudDatabase Create(iCloudDatabaseConfig config)
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
                        databases[service] = new Dictionary<string,iCloudDatabase>();
                        
                    var db = config.GetConfig<string>(Firebase.FirebaseCloudDatabaseConfig.DB);
                    if(db.IsNullOrEmpty())
                        db = "default";

                    var dic = databases[service] as Dictionary<string,iCloudDatabase>;
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
        public static iCloudDatabase Get(CloudDatabaseType type,params string[] param)
        {
            switch(type)
            {
                #if FIREBASE_REALTIME_DATABASE
                case CloudDatabaseType.FirebaseRealtimeDatabase:
                {
                    if(databases.ContainsKey(type))
                    {
                        var services = databases[type] as Dictionary<string,iCloudDatabase>;
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
        public static async Task<iCloudDatabase> GetAsync(CloudDatabaseType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
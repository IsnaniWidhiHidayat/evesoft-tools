#if ODIN_INSPECTOR 
using System.Collections.Generic;

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

        public static iCloudDatabase CreateDatabase(iCloudDatabaseConfig config)
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
    }
}
#endif
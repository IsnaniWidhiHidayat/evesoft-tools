#if ODIN_INSPECTOR 
using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudDatabaseOptionsFactory
    {
        #if FIREBASE_REALTIME_DATABASE
        public static IDictionary<string,object> CreateFirebaseDatabaseOptions(string dbPathRefence)
        {
            return new Dictionary<string,object>()
            {
                {Firebase.FirebaseCloudDatabaseConfig.REF,dbPathRefence}
            };
        }
        #endif
    }
}
#endif
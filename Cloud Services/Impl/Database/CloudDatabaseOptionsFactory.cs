using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudDatabaseOptionsFactory
    {
        public static IDictionary<string,object> CreateFirebaseDatabaseOptions(string dbPathRefence)
        {
            return new Dictionary<string,object>()
            {
                {Firebase.FirebaseCloudDatabaseConfig.REF,dbPathRefence}
            };
        }
    }
}
// using System.Collections.Generic;

// namespace Evesoft.CloudService
// {
//     public static class CloudDatabaseFactory
//     {
//         const string defaultKey = "default";

//         private static IDictionary<CloudDatabaseType,iCloudDatabase> databases = new Dictionary<CloudDatabaseType,iCloudDatabase>();

//         public static iCloudDatabase CreateDatabase(iCloudDatabaseConfig config)
//         {
//             if(config.IsNull())
//                 return null;

//             var service = config.GetConfig<CloudDatabaseType>(nameof(CloudService));
            
//             if(databases.ContainsKey(service))
//                 return databases[service];

//             switch(service)
//             {
//                 case CloudDatabaseType.FirebaseRealtimeDatabase:
//                 {
//                     var outurl  = default(object);
//                     var url     = default(string);
//                     config.TryGetValue(nameof(url),out outurl);
//                     url         = outurl as string;

//                     var key  = url.IsNullOrEmpty()? defaultKey : url;
//                     if(databases.ContainsKey(key))
//                         return databases[key];

//                     var newDatabase = new Firebase.FirebaseCloudDatabase(url);

//                     databases[key] = newDatabase;
//                     return newDatabase;
//                 }

//                 default:
//                 {
//                     "Service UnAvailable".LogError();
//                     return null;
//                 }
//             }
//         }
//     }
// }
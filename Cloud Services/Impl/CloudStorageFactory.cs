// namespace Evesoft.CloudService
// {
//     public static class CloudStorageFactory
//     {
//         private static IDictionary<string,iCloudStorage> storages;
        
       
//         public static iCloudStorage CreateStorage(CloudServiceType type,IDictionary<string,object> config)
//         {
//             if(storages.IsNull())
//                 storages = new Dictionary<string,iCloudStorage>();

//             switch(type)
//             {
//                 case CloudServiceType.Firebase:
//                 {
//                     var outurl  = default(object);
//                     var url     = default(string);
//                     config.TryGetValue(nameof(url),out outurl);
//                     url         = outurl as string;

//                     var key  = url.IsNullOrEmpty()? "default" : url;
//                     if(storages.ContainsKey(key))
//                         return storages[key];

//                     var newStorage = new FirebaseCloudStorage(url);
                    
//                     storages[key] = newStorage;
//                     return newStorage;
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
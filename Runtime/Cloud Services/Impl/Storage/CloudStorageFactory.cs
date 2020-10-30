#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.CloudService
{
    public static class CloudStorageFactory
    {
        private static IDictionary<CloudStorageType,ICloudStorage> storages = new Dictionary<CloudStorageType,ICloudStorage>();
        
        public static ICloudStorage Create(ICloudStorageConfig config)
        {
            if(config.IsNull())
                return null;

            var service = config.GetConfig<CloudStorageType>(nameof(CloudService));
            if(storages.ContainsKey(service))
                return storages[service];

            switch(service)
            {
                #if FIREBASE_STORAGE
                case CloudStorageType.FirebaseStorage:
                {
                    return storages[service] = new Firebase.FirebaseCloudStorage(config);
                }
                #endif
                
                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
        public static ICloudStorage Get(CloudStorageType type)
        {
            if(storages.ContainsKey(type))
                return storages[type];

            return null;
        }
        public static async Task<ICloudStorage> GetAsync(CloudStorageType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }
    }
}
#endif
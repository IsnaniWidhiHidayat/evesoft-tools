using System.Collections.Generic;

namespace Evesoft.CloudService
{
    public static class CloudStorageFactory
    {
        private static IDictionary<CloudStorageType,iCloudStorage> storages = new Dictionary<CloudStorageType,iCloudStorage>();
        
        public static iCloudStorage CreateStorage(iCloudStorageConfig config)
        {
            if(config.IsNull())
                return null;

            var service = config.GetConfig<CloudStorageType>(nameof(CloudService));
            if(storages.ContainsKey(service))
                return storages[service];

            switch(service)
            {
                case CloudStorageType.FirebaseStorage:
                {
                    return storages[service] = new Firebase.FirebaseCloudStorage(config);
                }

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
    }
}
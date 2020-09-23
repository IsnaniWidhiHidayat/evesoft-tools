using System.Collections.Generic;

namespace RollingGlory.FaceApp
{
    public static class CloudRoomOptionsFactory
    {
        public static IDictionary<string,object> CreateFirebaseRoomOptions(iCloudDatabase database,iCloudStorage storage,string databaseroot,string storageroot)
        {
            var options = new Dictionary<string,object>()
            {
                {nameof(database),database},
                {nameof(storage),storage},
                {nameof(databaseroot),databaseroot},
                {nameof(storageroot),storageroot}
            };
            return options;
        }      
    }
}
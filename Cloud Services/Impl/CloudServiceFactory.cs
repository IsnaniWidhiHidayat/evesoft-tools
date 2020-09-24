using System.Collections.Generic;
using Evesoft;

namespace Evesoft.CloudService
{
    public static class CloudServiceFactory
    {
        private static IDictionary<string,iCloudDatabase> databases;
        private static IDictionary<string,iCloudStorage> storages;
        private static IDictionary<string,iCloudAuth> auths;
        private static IDictionary<string,iCloudRoom> rooms;

        public static iCloudAuth CreateAuth(CloudServiceType type,IDictionary<string,object> options)
        {
            if(auths.IsNull())
                auths = new Dictionary<string,iCloudAuth>();

            var key = type.ToString();
            if(auths.ContainsKey(key))
                return auths[key];

            switch(type)
            {
                case CloudServiceType.Google:
                {
                    var webclientid = default(object);
                    options.TryGetValue(nameof(webclientid),out webclientid);
                    var newAuth = new GoogleSignIn.GoogleAuth(webclientid as string);   
                    auths[key] = newAuth;
                    return newAuth;
                }

                case CloudServiceType.Facebook:
                {
                    var newAuth = new FacebookAuth();   
                    auths[key] = newAuth;
                    return newAuth;
                }

                case CloudServiceType.Firebase:
                {
                    var newAuth = new FirebaseCloudAuth();   
                    auths[key] = newAuth;
                    return newAuth;
                }

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
        public static iCloudDatabase CreateDatabase(CloudServiceType type,IDictionary<string,object> options)
        {
            if(databases.IsNull())
                databases = new Dictionary<string,iCloudDatabase>();

            switch(type)
            {
                case CloudServiceType.Firebase:
                {
                    var outurl  = default(object);
                    var url     = default(string);
                    options.TryGetValue(nameof(url),out outurl);
                    url         = outurl as string;

                    var key  = url.IsNullOrEmpty()? "default" : url;
                    if(databases.ContainsKey(key))
                        return databases[key];

                    var newDatabase = new FirebaseCloudDatabase(url);

                    databases[key] = newDatabase;
                    return newDatabase;
                }

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
        public static iCloudStorage CreateStorage(CloudServiceType type,IDictionary<string,object> options)
        {
            if(storages.IsNull())
                storages = new Dictionary<string,iCloudStorage>();

            switch(type)
            {
                case CloudServiceType.Firebase:
                {
                    var outurl  = default(object);
                    var url     = default(string);
                    options.TryGetValue(nameof(url),out outurl);
                    url         = outurl as string;

                    var key  = url.IsNullOrEmpty()? "default" : url;
                    if(storages.ContainsKey(key))
                        return storages[key];

                    var newStorage = new FirebaseCloudStorage(url);
                    
                    storages[key] = newStorage;
                    return newStorage;
                }

                default:
                {
                    "Service UnAvailable".LogError();
                    return null;
                }
            }
        }
        // public static iCloudRoom CreateRoom(CloudServiceType type,IDictionary<string,object> options)
        // {
        //     if(rooms.IsNull())
        //         rooms = new Dictionary<string,iCloudRoom>();

        //     var key = type.ToString();
        //     if(rooms.ContainsKey(key))
        //         return rooms[key];

        //     switch(type)
        //     {
        //         case CloudServiceType.Firebase:
        //         {  
        //             // Get Config
        //             var databaseroot        = default(string);
        //             var storageroot         = default(string);
        //             var database            = default(iCloudDatabase);
        //             var storage             = default(iCloudStorage); 
        //             var outdatabaseroot     = default(object);
        //             var outstorageroot      = default(object);
        //             var outdatabase         = default(object);
        //             var outstorage          = default(object);      
                    
        //             options.TryGetValue(nameof(databaseroot),out outdatabaseroot);
        //             options.TryGetValue(nameof(storageroot),out outstorageroot);
        //             options.TryGetValue(nameof(database),out outdatabase);
        //             options.TryGetValue(nameof(storage),out outstorage);

        //             databaseroot        = outdatabaseroot as string;
        //             storageroot         = outstorageroot  as string;
        //             database            = outdatabase     as iCloudDatabase;
        //             storage             = outstorage      as iCloudStorage; 
                
        //             var newRoom  = new FirebaseCloudRoom(database,storage,databaseroot,storageroot);
        //             rooms[key] = newRoom;
        //             return newRoom;
        //         }

        //         default:
        //         {
        //             return null;
        //         }
        //     }
        // }     
    
    }
}
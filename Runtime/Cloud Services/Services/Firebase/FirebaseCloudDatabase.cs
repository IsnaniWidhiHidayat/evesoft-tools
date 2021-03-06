#if ODIN_INSPECTOR 
#if FIREBASE_REALTIME_DATABASE
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    internal class FirebaseCloudDatabase : ICloudDatabase
    {
        #region private
        private static bool _inited;
        private bool _initing;
        private FirebaseDatabase _database;
        #endregion
        
        #region methods
        private async void Init(string url)
        {
            if(_inited || _initing)
                return;

            _initing = true;
            var status = await FirebaseDependencies.CheckAndFixDependencies();

            switch(status)
            {
                case DependencyStatus.Available:
                {   
                    _database = url.IsNullOrEmpty()? FirebaseDatabase.DefaultInstance : FirebaseDatabase.GetInstance(url);
                    _initing = false;
                    _inited = true;
                    break;
                }

                default:
                {
                    _initing = false;
                    _inited = false;
                    break;
                }
            }
        }
        #endregion

        #region iCloudDatabase
        public async Task<(ICloudDatabaseReference, Exception)> Connect(IDictionary<string, object> parameter)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(parameter.IsNull())
                    return (null,new ArgumentNullException(nameof(parameter)));
                
                var pathKey = FirebaseCloudDatabaseConfig.REF;

                if(!parameter.ContainsKey(pathKey))
                    return (null,new ArgumentNullException(pathKey));
                
                var path  = parameter[pathKey] as string;
                var dbRef = _database.GetReference(path);
                
                if(dbRef.IsNull())
                    return (null,new NullReferenceException(nameof(dbRef)));
                
                var reference = new FirebaseCloudDatabaseReference(dbRef);
                await new WaitUntil(()=> reference.inited);

                return (reference,null);
            } 
            catch (DatabaseException ex) 
            {
                return (null,ex);
            }
        }
        public async Task<Exception> Disconnect(ICloudDatabaseReference reference)
        {
            if(reference.IsNull())
                return new ArgumentNullException(nameof(reference));

            await new WaitForEndOfFrame();
            reference.Dispose();
            return null;
        }
        #endregion

        #region Constructor
        public FirebaseCloudDatabase(ICloudDatabaseConfig config)
        {
            var url = config?.GetConfig<string>(FirebaseCloudDatabaseConfig.DB);
            Init(url);
        }
        #endregion
    }
}
#endif
#endif
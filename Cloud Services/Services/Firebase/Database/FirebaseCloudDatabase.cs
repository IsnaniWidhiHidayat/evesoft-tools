using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Evesoft;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace RollingGlory.FaceApp
{
    [Serializable,HideReferenceObjectPicker]
    public class FirebaseCloudDatabase : iCloudDatabase
    {
        #region private
        private static bool _inited;
        private bool _initing;
        private FirebaseDatabase _database;
        #endregion

        #region iCloudDatabase
        public bool inited => _inited;

        public async Task<(iCloudDatabaseData,Exception)> GetData(string path)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);

                if(path.IsNullOrEmpty())
                    return (null,new System.ArgumentNullException("path"));

                var reference = _database.GetReference(path);
                var data = await reference.GetValueAsync();    
                return(!data.IsNull() && data.Exists? new FirebaseCloudDatabaseData(data) : null,null);
            } 
            catch (DatabaseException ex) 
            {
                return (null,ex);
            }
        }
        public async Task<Exception> SetData(object data, string path,DatabaseDataType dataType)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("path");

                switch(dataType)
                {
                    case DatabaseDataType.Object:
                    {
                        var reference = _database.GetReference(path);
                        await reference.SetValueAsync(data);
                        break;
                    }
    
                    case DatabaseDataType.json:
                    {
                        var reference = _database.GetReference(path);
                        var json = data as string;
                        await reference.SetRawJsonValueAsync(json);
                        break;
                    }
                }

                return null;
            } 
            catch (DatabaseException ex) 
            {
                return ex;
            }
        }
        public async Task<Exception> RemoveData(string path)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("path");

                var reference = _database.GetReference(path);
                await reference.RemoveValueAsync();
                return null;
            } 
            catch (DatabaseException ex) 
            {
                return ex;
            }
        }
        public async Task<Exception> UpdateData(IDictionary<string, object> data, string path)
        {
           try 
           {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(path.IsNullOrEmpty())
                    return new System.ArgumentNullException("path");
    
                var reference = _database.GetReference(path);
                await reference.UpdateChildrenAsync(data);
                return null;
           } 
           catch (DatabaseException ex) 
           {
               return ex;
           }
        }
        public async Task<(iCloudDatabaseReference,Exception)> Reference(string parameter)
        {
            try 
            {
                if(!_inited)
                    await new WaitUntil(()=> _inited);
    
                if(parameter.IsNull())
                    return (null,new System.ArgumentNullException("parameter"));
                
                var dbRef = _database.GetReference(parameter);
                return (new FirebaseCloudDatabaseReference(dbRef),null);
            } 
            catch (DatabaseException ex) 
            {
                return (null,ex);
            }
        }
        #endregion

        #region Constructor
        public FirebaseCloudDatabase()
        {
            Init(null);
        }
        public FirebaseCloudDatabase(string url)
        {
            Init(url);
        }
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
    }
}
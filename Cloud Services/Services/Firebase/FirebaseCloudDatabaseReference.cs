using System;
using Firebase.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Evesoft.CloudService.Firebase
{
    [Serializable,HideReferenceObjectPicker]
    internal class FirebaseCloudDatabaseReference : iCloudDatabaseReference,IDisposable
    {      
        #region private
        private iCloudDatabaseEvents _events;
        private IDictionary<string, object> _reference;
        private IDictionary<string, object> _data;
        private DatabaseReference _dbRef;
        private bool _inited;
        #endregion

        #region property
        public bool inited => _inited;
        #endregion

        #region iCloudDatabaseReference
        public iCloudDatabaseEvents events =>_events;

        [ShowInInspector,ReadOnly,ShowIf(nameof(data))]
        public IDictionary<string, object> data => _data;

        [Button] public async Task<Exception> SetData(IDictionary<string, object> value)
        {
            if(value.IsNull())
                return null;

            try 
            {
                IList<(string,object)> data = new List<(string,object)>();
                GetRefKey(value,ref data);

                foreach (var item in data)
                    await _dbRef?.Child(item.Item1).SetValueAsync(item.Item2);

                return null;
            } 
            catch (DatabaseException ex) 
            {
                return ex;
            }
        }
        [Button] public async Task<Exception> RemoveData(IDictionary<string, object> value)
        {
            if(value.IsNull())
                return null;

            try 
            {
                IList<(string,object)> data = new List<(string,object)>();
                GetRefKey(value,ref data);

                foreach (var item in data)
                    await _dbRef?.Child(item.Item1).RemoveValueAsync();

                return null;
            } 
            catch (DatabaseException ex) 
            {
                return ex;
            }
        }
        [Button] public async Task<Exception> UpdateData(IDictionary<string, object> value)
        {
            if(value.IsNull())
                return null;

            try 
            {
                await _dbRef.UpdateChildrenAsync(value);
                return null;
            } 
            catch (DatabaseException ex) 
            {
                return ex;
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            RemoveReferenceEvents(_dbRef);

            _events?.Dispose();
            _reference?.Dispose();
            _data?.Dispose();

            _events     = null;
            _reference  = null;
            _data       = null;
        }
        #endregion

        #region methods
        private void SetReferenceEvents(DatabaseReference reference)
        {
            RemoveReferenceEvents(reference);
            if(reference.IsNull())
                return;

            reference.ValueChanged  += OnValueChange;
            reference.ChildAdded    += OnChildAdded;
            reference.ChildChanged  += OnChildChange;
            reference.ChildRemoved  += OnChildRemoved;
        }
        private void RemoveReferenceEvents(DatabaseReference reference)
        {
            if(reference.IsNull())
                return;

            reference.ValueChanged  -= OnValueChange;
            reference.ChildAdded    -= OnChildAdded;
            reference.ChildChanged  -= OnChildChange;
            reference.ChildRemoved  -= OnChildRemoved;
        }
        private void GetRefKey(IDictionary<string,object> dic,ref IList<(string,object)> result,string prefPath = null)
        {
            foreach (var item in dic)
            {
                var key     = item.Key;
                var value   = item.Value;
                var child   = value as IDictionary<string,object>;
        
                var path  = prefPath + "/" + key;

                if(child.IsNull())
                {
                    result.Add((path,value));
                }
                else
                {    
                    GetRefKey(child,ref result,path);
                }
            }
        }
        #endregion
        
        #region callbacks  
        private void OnValueChange(object sender, ValueChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            _data = value as IDictionary<string,object>;

            if(_data.IsNull())
            {
                _data = new Dictionary<string,object>();
                _data[key] = value;
            }
           
            if(!_inited)
                _inited = true;

            var events = this.events as FirebaseCloudDatabaseEvents;
                events?.OnDataChange(key,value);
        }
        private void OnChildAdded(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;
            
            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            var events = this.events as FirebaseCloudDatabaseEvents;
                events?.OnDataAdded(key,value);
        }
        private void OnChildChange(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            var events = this.events as FirebaseCloudDatabaseEvents;
                events?.OnDataChange(key,value);
        }
        private void OnChildRemoved(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return; 

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            var events = this.events as FirebaseCloudDatabaseEvents;
                events?.OnDataRemoved(key,value);
        }
        #endregion

        #region constructor
        public FirebaseCloudDatabaseReference(DatabaseReference dbRef)
        {
            _dbRef = dbRef;
            _events = new FirebaseCloudDatabaseEvents();
            SetReferenceEvents(_dbRef);
        }
        #endregion
    }
}
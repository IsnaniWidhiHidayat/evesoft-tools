using System;
using EveSoft;
using Firebase.Database;

namespace RollingGlory.FaceApp
{
    [Serializable]
    public class FirebaseCloudDatabaseReference : iCloudDatabaseReference,IDisposable
    {
        #region private
        private DatabaseReference _reference;
        #endregion

        #region iCloudDatabaseReference
        public event Action<(string,object)> onDataAdded;
        public event Action<(string,object)> onDataChange;
        public event Action<(string,object)> onDataRemoved;
        #endregion

        #region IDisposable
        public void Dispose()
        {
            RemoveReferenceEvents(_reference);
            onDataAdded     = null;
            onDataChange    = null;
            onDataRemoved   = null;
        }
        #endregion

        #region methods
        private void SetReferenceEvents(DatabaseReference reference)
        {
            RemoveReferenceEvents(reference);
            if(reference.IsNull())
                return;

            reference.ValueChanged  += onValueChange;
            reference.ChildAdded    += OnChildAdded;
            reference.ChildChanged  += OnChildChange;
            reference.ChildRemoved  += OnChildRemoved;
        }
        private void RemoveReferenceEvents(DatabaseReference reference)
        {
            if(reference.IsNull())
                return;

            reference.ValueChanged  -= onValueChange;
            reference.ChildAdded    -= OnChildAdded;
            reference.ChildChanged  -= OnChildChange;
            reference.ChildRemoved  -= OnChildRemoved;
        }
        #endregion
        
        #region callbacks  
        private void onValueChange(object sender, ValueChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            onDataChange?.Invoke((key,value));
        }
        private void OnChildAdded(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;
            
            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            onDataAdded?.Invoke((key,value));
        }
        private void OnChildChange(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return;

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            onDataChange?.Invoke((key,value));
        }
        private void OnChildRemoved(object sender, ChildChangedEventArgs e)
        {
            if(e.Snapshot.IsNull())
                return; 

            var key     = e.Snapshot.Key;
            var value   = e.Snapshot.Value;

            onDataRemoved?.Invoke((key,value));
        }
        #endregion

        #region constructor
        public FirebaseCloudDatabaseReference(DatabaseReference reference)
        {
            _reference = reference;
            SetReferenceEvents(_reference);
        }
        #endregion
    }
}
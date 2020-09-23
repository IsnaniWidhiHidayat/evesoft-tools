using System;
using System.Collections.Generic;
using EveSoft;
using Firebase.Database;
using Sirenix.OdinInspector;

namespace RollingGlory.FaceApp
{
    [Serializable]
    public class FirebaseCloudDatabaseData : iCloudDatabaseData,IDisposable
    {
        #region private
        private (string,object) _data;
        private DataSnapshot _snaphot;
        #endregion

        #region iCloudDatabaseData
        [ShowInInspector]
        public (string,object) data => _data;
        public string ToJson()
        {
            return _snaphot.GetRawJsonValue();
        }
        #endregion

        #region constructor
        public FirebaseCloudDatabaseData(DataSnapshot snaphot)
        {
            _snaphot = snaphot;
            if(!_snaphot.IsNull() && _snaphot.Exists)
                _data = (_snaphot.Key,_snaphot.Value);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _data = (null,null);
        }
        #endregion
    }
}
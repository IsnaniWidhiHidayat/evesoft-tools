#if ODIN_INSPECTOR 
#if FIREBASE_STORAGE
using System.Collections.Generic;

namespace Evesoft.CloudService.Firebase
{
    internal class FirebaseCloudStorageConfig : ICloudStorageConfig
    {
        #region const
        public const string STORAGE = nameof(STORAGE);
        #endregion

        #region private
        private IDictionary<string, object> _configs;
        #endregion

        #region iCloudStorageConfig
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region constructor
        public FirebaseCloudStorageConfig()
        {
            _configs = new Dictionary<string,object>();
            _configs[nameof(CloudService)] = CloudStorageType.FirebaseStorage;
        }
        public FirebaseCloudStorageConfig(string storage):this()
        {
            _configs[STORAGE] = storage;
        }
        #endregion
    }
}
#endif
#endif
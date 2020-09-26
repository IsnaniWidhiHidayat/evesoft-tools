#if CACHE_TEXTURE2D
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using Sirenix.OdinInspector;

namespace Evesoft.Cache
{
    [Serializable]
    public class Texture2DCache : iCache<Texture2DCacheData>,IDisposable
    {
        #region const
        const string defaultKey = "default";
        #endregion

        #region Private
        private string _fileFormat;
        private string _fileLocation;
        private List<Texture2DCacheData> _saved;
        #endregion

        #region Static
        private static iCache<Texture2DCacheData> _defaultInstance;  
        private static Dictionary<string,iCache<Texture2DCacheData>> _caches = new Dictionary<string, iCache<Texture2DCacheData>>();
        public static iCache<Texture2DCacheData> defaultInstance
        {
            get
            {
                if(!_defaultInstance.IsNull())
                {
                    return _defaultInstance;
                }
                else
                    return _defaultInstance = GetCache(defaultKey);
            }
        }
        public static iCache<Texture2DCacheData> GetCache(string key)
        {
            if(key.IsNullOrEmpty())
                return null;

            if(_caches.ContainsKey(key))
                return _caches[key];
            else
                return null;
        }
        public static Dictionary<string,iCache<Texture2DCacheData>> GetCaches()
        {
            return _caches;
        }
        public static iCache<Texture2DCacheData> CreateCache(string key = defaultKey,string metaFile = "images/images.json",string format = ".png")
        {
            var cache = GetCache(key);
            if(!cache.IsNull())
                return cache;

            return _caches[key] = new Texture2DCache(metaFile,format);
        }
        public static void RemoveCache(string key)
        {
            var cache = GetCache(key);
            if(!cache.IsNull())
                _caches.Remove(key);

            cache.Dispose();
        }

        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            _caches?.Clear();
            _defaultInstance = CreateCache();
            "Default Texture2D Cache Loaded".Log();
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if(!cached.IsNullOrEmpty())
                foreach(var cache in cached)
                    cache?.Dispose();
                
            cached?.Clear();
            _fileLocation?.RemoveFile();   
        }
        #endregion
       
        #region iCache
        public event Action<iCache<Texture2DCacheData>> onLoaded;
        public event Action<iCache<Texture2DCacheData>> onSaved;

        [Newtonsoft.Json.JsonProperty(nameof(cached))]
        private List<Texture2DCacheData> _cached;

        [Newtonsoft.Json.JsonIgnore,ShowInInspector]
        public List<Texture2DCacheData> cached => _cached;

        public Texture2DCacheData GetCache(string url,string key)
        {
            if((url.IsNullOrEmpty() && key.IsNullOrEmpty()) || cached.IsNullOrEmpty())
                return null;
 
            //Get from Cache     
            var cache = cached.Find(x => (x.key == key || x.url == url));
            if(cache.IsNull())
                return null;

            if(cache.url != url)
            {
                RemoveCache(cache);
                return null;
            }
            else
            {
                return cache;
            }
        }   
        public void AddCache(Texture2DCacheData cache)
        {
            if(cache.IsNull())
                return;

            if(!cached.IsNullOrEmpty() && cached.Contains(cache))
                return;

            if(_cached.IsNull())
                _cached = new List<Texture2DCacheData>();

            cached.Add(cache);
            Save();
        }
        public void RemoveCache(Texture2DCacheData cache)
        {
            if(cache.IsNull())
                return;

            RemoveCacheFile(cache);
            cached.Remove(cache);
            _saved.Remove(cache);
            Save();
        }
        public void UpdateCache(Texture2DCacheData cache)
        {
            //Remove Picture file from disk
            RemoveCacheFile(cache);

            //ReSave File
            SaveCacheFile(cache);
                
            if(!_saved.IsNullOrEmpty() && _saved.Contains(cache))
                _saved.Remove(cache);

            Save();
        }
        public void Save()
        {
            if (cached.IsNullOrEmpty() || _fileLocation.IsNullOrEmpty())
                return;

            //Write image file to disk
            foreach (var cache in cached)
            {
                if(_saved.IsNull())
                    _saved = new List<Texture2DCacheData>();

                if (cache.IsNull() || cache.data.IsNull() || _saved.Contains(cache))
                    continue;

                SaveCacheFile(cache);
                _saved.Add(cache);
            }

            //Save Json File
            (var json,var exception) = this.ToJson();
            if(!json.IsNullOrEmpty())
                json.WriteToFile(_fileLocation);

            onSaved?.Invoke(this);
        }
        public void Load()
        {
            //Check Meta File exist
            if(_fileLocation.FileExist())
            {
                var jsonText = _fileLocation.ReadFile();
                (var imageCache, var exception) = jsonText.FromJson<Texture2DCache>();
                if(!imageCache.IsNull())
                {
                    _cached.Dispose();
                    _cached = imageCache.cached;
                }
            }

            if(!cached.IsNullOrEmpty()) 
            {
                var markDeletedCache = default(List<Texture2DCacheData>);
    
                //Load File from disk
                foreach (var cache in cached)
                {
                    var path = GetFilePath(cache.key);

                    //Check file not found
                    if (!path.FileExist())
                    {
                        if (markDeletedCache.IsNull())
                            markDeletedCache = new List<Texture2DCacheData>();
    
                        markDeletedCache.Add(cache);
                        continue;
                    }
    
                    LoadCacheFile(cache);
                }
    
                //Delete all mark deleted picture
                if (!markDeletedCache.IsNullOrEmpty())
                {
                    foreach (var picture in markDeletedCache)
                    {
                        picture.Dispose();
                        cached.Remove(picture);
                    }
    
                    markDeletedCache.Clear();
                    markDeletedCache = null;
                }
            }

            onLoaded?.Invoke(this);
        }       
        #endregion

        #region Constructor
        public Texture2DCache(){}
        public Texture2DCache(string metaFile,string format)
        {
            this._fileFormat   = format;
            this._fileLocation = Application.temporaryCachePath.CombinePath(metaFile);
            var _directory    = Path.GetDirectoryName(_fileLocation);
                _directory.CreateDirectoryIfNotExist();

            this.Load();
        }
        #endregion

        #region Methods
        private void SaveCacheFile(Texture2DCacheData cache)
        {
            var data = cache.data.EncodeToPNG();
            if (data.IsNullOrEmpty())
                return;

            var path = GetFilePath(cache.key);
            data.WriteToFile(path);
        }
        private void LoadCacheFile(Texture2DCacheData cache)
        {
            var path = GetFilePath(cache.key);
            var bytes = path.ReadFileBytes();
            if (bytes.IsNullOrEmpty())
                return;

            cache.data = bytes.ToTexture2D(true);
        }
        private void RemoveCacheFile(Texture2DCacheData cache)
        {
            var path = GetFilePath(cache.key);
            path.RemoveFile();
        }
        private string GetFilePath(string key)
        {
            var directory = Path.GetDirectoryName(_fileLocation);
            var fileName = string.Format("{0}{1}",key,_fileFormat);
            return directory.CombinePath(fileName);
        }
        #endregion
    }   
}
#endif
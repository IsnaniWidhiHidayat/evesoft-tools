#if CACHE_TEXTURE2D
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json;

namespace Evesoft.Cache
{
    [Serializable]
    public class Texture2DCacheData : IDisposable
    {
        #region const
        const string grp = "$key";
        #endregion

        #region private
        private Texture2D _data;
        #endregion

        #region iCacheData
        [ShowInInspector,FoldoutGroup(grp),DisplayAsString]
        public string url {get;set;}

        [ShowInInspector,FoldoutGroup(grp),DisplayAsString]
        public string key {get;set;}

        [JsonIgnore,ShowInInspector,HideLabel,FoldoutGroup(grp),InlineEditor(inlineEditorMode : InlineEditorModes.LargePreview)]
        public Texture2D data {
            get => _data;
            set 
            {
                _data = value;

                if(!_data.IsNull())
                    _data.name = key;
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            data.Dispose();
            data = null;
        }
        #endregion

        #region constructor
        public Texture2DCacheData(){}
        public Texture2DCacheData(string url,string key,Texture2D data)
        {
            this.url = url;
            this.key = key;
            this.data = data;
        }
        #endregion
    }
}
#endif
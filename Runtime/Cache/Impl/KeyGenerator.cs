using System.Collections.Generic;
using System;

namespace Evesoft.Cache
{
    public struct KeyGenerator : IDisposable
    {
        #region private
        private IList<string> _keywords;
        private string _separator;
        #endregion

        #region methods
        public KeyGenerator AddKey(string keyword)
        {   
            if(keyword.IsNullOrEmpty())
                return this;

            if(_keywords.IsNull())
                _keywords = new List<string>();

            _keywords.Add(keyword);
            return this;
        }
        public KeyGenerator AddKeys(params string[] keywords)
        {
            if (keywords.IsNullOrEmpty())
                return this;

            foreach (var keyword in keywords)
            {
                if(keyword.IsNullOrEmpty())
                    continue;

                AddKey(keyword);
            }
            return this;
        }
        public KeyGenerator AddRandom()
        {
            var key =  Guid.NewGuid().ToString("N");
            return AddKey(key);
        }
        public string Build()
        {
            var result = string.Empty;

            for (int i = 0; i < _keywords.Count; i++)
            {
                if (_keywords[i].IsNullOrEmpty())
                    continue;

                result += string.Format("{0}{1}",_keywords[i],i < _keywords.Count-1? _separator : "");
            }

            return result;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if(!_keywords.IsNull())
                _keywords.Clear();
        } 
        #endregion

        #region constructor
        public KeyGenerator(string separator = "-")
        {
            _separator = separator;
            _keywords = null;
        }
        #endregion
    }
}

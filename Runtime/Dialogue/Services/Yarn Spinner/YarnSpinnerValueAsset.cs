#if ODIN_INSPECTOR && YARN_SPINNER
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;

namespace Evesoft.Dialogue.YarnSpinner
{
    public struct YarnSpinnerValue
    {
        public enum Type
        {
            Number,
            String,
            Bool
        }

        public string name;
        public string value;
        public Type type;
    }

    [CreateAssetMenu(menuName = nameof(Evesoft) + "/" + nameof(Evesoft.Dialogue) + "/" + nameof(Evesoft.Dialogue.YarnSpinner) + "/" + nameof(YarnSpinnerValueAsset), fileName = nameof(YarnSpinnerValueAsset))]
    public class YarnSpinnerValueAsset : SerializedScriptableObject,IDictionary<string,object>
    {
        #region field
        [SerializeField,TableList(AlwaysExpanded = true)]
        private List<YarnSpinnerValue> _defaultVariables = new List<YarnSpinnerValue>();
        #endregion

        #region private
        private IDictionary<string,object> _values = new Dictionary<string,object>();
        
        [System.NonSerialized]
        private bool _inited;
        #endregion

        #region property
        private IDictionary<string,object> values
        {
            get
            {
                if(_inited)
                    return _values;

                _values.Clear();
                foreach (var item in _defaultVariables)
                {
                    switch(item.type)
                    {
                        case YarnSpinnerValue.Type.Number:
                        {
                            _values[item.name] = item.value.ToSingle();
                            break;
                        }

                        case YarnSpinnerValue.Type.Bool:
                        {
                            _values[item.name] = item.value.ToBoolean();
                            break;
                        }

                        case YarnSpinnerValue.Type.String:
                        {
                            _values[item.name] = item.value;
                            break;
                        }
                    }
                }

                _inited = true;
                return _values;
            }
        }
        #endregion

        #region IDictionary
        public ICollection<object> Values => values.Values;
        public ICollection<string> Keys => values.Keys;
        public object this[string key] { get => values[key]; set => values[key] = value; }
        public int Count => values.Count;
        public bool IsReadOnly => values.IsReadOnly;
        public void Add(string key, object value)
        {
            values.Add(key, value);
        }
        public void Add(KeyValuePair<string, object> item)
        {
            values.Add(item);
        }
        public void Clear()
        {
            values.Clear();
        }
        public bool Contains(KeyValuePair<string, object> item)
        {
            return values.Contains(item);
        }
        public bool ContainsKey(string key)
        {
            return values.ContainsKey(key);
        }
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            values.CopyTo(array, arrayIndex);
        }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return values.GetEnumerator();
        }
        public bool Remove(string key)
        {
            return values.Remove(key);
        }
        public bool Remove(KeyValuePair<string, object> item)
        {
            return values.Remove(item);
        }
        public bool TryGetValue(string key, out object value)
        {
            return values.TryGetValue(key, out value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)values).GetEnumerator();
        }
        #endregion
    }
}
#endif

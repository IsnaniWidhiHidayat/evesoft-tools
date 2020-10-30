#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue.YarnSpinner
{
    internal class YarnSpinnerConfig : IDialogueConfig
    {    
        #region const
        public const string UI         = nameof(UI);
        public const string START_NODE = nameof(START_NODE);
        public const string START_AUTO = nameof(START_AUTO);
        public const string DEFAULT_VARIABLES_STORAGE = nameof(DEFAULT_VARIABLES_STORAGE);
        public const string DATA       = nameof(DATA);
        #endregion

        #region private
        private IDictionary<string,object> _configs;
        #endregion

        #region iAdsConfig
        public T GetConfig<T>(string key)
        {
            var result = default(T);
            if(_configs.ContainsKey(key) && _configs[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region methods
        public void SetUI(IDialogueUI ui)
        {
            _configs[UI] = ui;
        }
        public void SetStartNode(string name)
        {
            _configs[START_NODE]  = name;   
        }
        public void SetAutoStart(bool value)
        {
            _configs[START_AUTO] = value;       
        }
        public void SetDefaultValue(IDictionary<string,object> values)
        {
            _configs[DEFAULT_VARIABLES_STORAGE] = values;
        }      
        public void SetData(YarnSpinnerData data)
        {
            _configs[DATA] = data;
        }
        #endregion

        #region constructor
        internal YarnSpinnerConfig()
        {
            _configs                    = new Dictionary<string,object>();
            _configs[nameof(Dialogue)]  = DialogueType.YarnSpinner;
        }
        #endregion
    }
}
#endif
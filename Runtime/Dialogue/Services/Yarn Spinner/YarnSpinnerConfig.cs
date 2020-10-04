#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue.YarnSpinner
{
    internal class YarnSpinnerConfig : iDialogueConfig
    {    
        #region const
        public const string UI         = nameof(UI);
        public const string START_NODE = nameof(START_NODE);
        public const string START_AUTO = nameof(START_AUTO);
        public const string SCRIPTS    = nameof(SCRIPTS);
        public const string ATTACH_TO  = nameof(ATTACH_TO);
        public const string DEFAULT_VARIABLES_STORAGE = nameof(DEFAULT_VARIABLES_STORAGE);
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
        public void SetUI(iDialogueUI ui)
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
        public void AddScripts(params YarnProgram[] scripts)
        {
            if(scripts.IsNullOrEmpty())
                return;

            if(!_configs.ContainsKey(SCRIPTS))
                _configs[SCRIPTS] = new List<YarnProgram>();

            var value = _configs[SCRIPTS] as List<YarnProgram>;
                value.AddRange(scripts);
        }
        public void SetAttachTo(GameObject obj)
        {
            _configs[ATTACH_TO]  = obj;
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
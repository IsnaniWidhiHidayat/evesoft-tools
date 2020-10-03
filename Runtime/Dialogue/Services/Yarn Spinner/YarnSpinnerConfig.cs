#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinnerConfig : iDialogueConfig
    {    
        #region const
        const string UI         = nameof(UI);
        const string START_NODE = nameof(START_NODE);
        const string START_AUTO = nameof(START_AUTO);
        const string SCRIPTS    = nameof(SCRIPTS);
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

        #region constructor
        public YarnSpinnerConfig(iDialogueUI ui,string startNode,bool startAuto,IList<YarnProgram> yarnScripts)
        {
            _configs                    = new Dictionary<string,object>();
            _configs[nameof(Dialogue)]  = DialogueType.YarnSpinner;
            _configs[UI]                = ui;
            _configs[START_NODE]        = startNode;
            _configs[START_AUTO]        = startAuto;
            _configs[SCRIPTS]           = yarnScripts;
        }
        #endregion
    }
}
#endif
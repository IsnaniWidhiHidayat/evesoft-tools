#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinnerData : iDialogueData
    {
        #region const
        const string SCRIPT                 = nameof(SCRIPT);
        const string COMMAND_HANDLER        = nameof(COMMAND_HANDLER);
        const string FUNCTION               = nameof(FUNCTION);
        const string RETURNING_FUNCTION     = nameof(RETURNING_FUNCTION);
        #endregion

        #region private
        private IDictionary<string,object> _data;    
        #endregion

        #region iDialogueData
        public T GetValue<T>(string key)
        {
             var result = default(T);
            if(_data.ContainsKey(key) && _data[key].To<T>(out result))
                return result;
                
            return result;
        }
        #endregion

        #region contructor
        public YarnSpinnerData(YarnProgram script,
        (string,DialogueRunner.CommandHandler) commanHandler,
        (string,int,Function) function,
        (string,int,ReturningFunction) returningFunction)
        {
            _data = new Dictionary<string,object>();
            _data[SCRIPT]               = script;
            _data[COMMAND_HANDLER]      = commanHandler;
            _data[FUNCTION]             = function;
            _data[RETURNING_FUNCTION]   = returningFunction;
        }

        public YarnSpinnerData(string removeCommandHandlerName,string removeFunctionName)
        {
            _data[COMMAND_HANDLER]      = removeCommandHandlerName;
            _data[FUNCTION]             = removeFunctionName;
        }
        #endregion
    }
}
#endif
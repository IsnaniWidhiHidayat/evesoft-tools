#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinnerData : iDialogueData
    {
        #region const
        public const string SCRIPT                 = nameof(SCRIPT);
        public const string STRING_TABLE           = nameof(STRING_TABLE);
        public const string COMMAND_HANDLER        = nameof(COMMAND_HANDLER);
        public const string REMOVE_COMMAND         = nameof(REMOVE_COMMAND);
        public const string FUNCTION               = nameof(FUNCTION);
        public const string REMOVE_FUNCTIONS       = nameof(REMOVE_FUNCTIONS);
        public const string RETURNING_FUNCTION     = nameof(RETURNING_FUNCTION);
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

        #region methods
        public void AddScript(params YarnProgram[] scripts)
        {
            var key = SCRIPT;
            if(!_data.ContainsKey(key))
                _data[key] = new List<YarnProgram>();

            var data = _data[key] as List<YarnProgram>;
                data.AddRange(scripts);
        }    
        public void AddStringTable(params IDictionary<string,Yarn.Compiler.StringInfo>[] tables)
        {
            var key = STRING_TABLE;
            if(!_data.ContainsKey(key))
                _data[key] = new List<IDictionary<string,Yarn.Compiler.StringInfo>>();

            var data = _data[key] as List<IDictionary<string,Yarn.Compiler.StringInfo>>;
                data.AddRange(tables);
        }
        public void AddCommandHandler(params (string,DialogueRunner.CommandHandler)[] handler)
        {
            var key = COMMAND_HANDLER;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,DialogueRunner.CommandHandler)>();

            var data = _data[key] as List<(string,DialogueRunner.CommandHandler)>;
                data.AddRange(handler);
        }
        public void AddFunctions(params (string,int,Yarn.Function)[] functions)
        {
            var key = FUNCTION;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,int,Yarn.Function)>();

            var data = _data[key] as List<(string,int,Yarn.Function)>;
                data.AddRange(functions);
        }
        public void AddFunctions(params (string,int,ReturningFunction)[] functions)
        {
            var key = RETURNING_FUNCTION;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,int,Yarn.ReturningFunction)>();

            var data = _data[key] as List<(string,int,Yarn.ReturningFunction)>;
                data.AddRange(functions);
        }
        public void RemoveCommand(params string[] names)
        {
            var key = REMOVE_COMMAND;
            if(!_data.ContainsKey(key))
                _data[key] = new List<string>();

            var data = _data[key] as List<string>;
                data.AddRange(names);
        }
        public void RemoveFunctions(params string[] names)
        {
            var key = REMOVE_FUNCTIONS;
            if(!_data.ContainsKey(key))
                _data[key] = new List<string>();

            var data = _data[key] as List<string>;
                data.AddRange(names);
        }
        #endregion

        #region constructor
        internal YarnSpinnerData()
        {
            _data = new Dictionary<string,object>();
        }
        #endregion
    }
}
#endif
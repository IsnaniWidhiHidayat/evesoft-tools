#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinnerData : IDialogueData
    {
        #region const
        public const string SCRIPT                 = nameof(SCRIPT);
        public const string STRING_TABLE           = nameof(STRING_TABLE);
        public const string COMMAND                = nameof(COMMAND);
        public const string BLOKING_COMMAND        = nameof(BLOKING_COMMAND); 
        public const string FUNCTION               = nameof(FUNCTION);
        // public const string REMOVE_COMMAND         = nameof(REMOVE_COMMAND);
        // public const string REMOVE_FUNCTIONS       = nameof(REMOVE_FUNCTIONS);
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
        public void AddScripts(params YarnProgram[] scripts)
        {
            var key = SCRIPT;
            if(!_data.ContainsKey(key))
                _data[key] = new List<YarnProgram>();

            var data = _data[key] as List<YarnProgram>;
                data.AddRange(scripts);
        }    
        public void AddStringTables(params IDictionary<string,Yarn.Compiler.StringInfo>[] tables)
        {
            var key = STRING_TABLE;
            if(!_data.ContainsKey(key))
                _data[key] = new List<IDictionary<string,Yarn.Compiler.StringInfo>>();

            var data = _data[key] as List<IDictionary<string,Yarn.Compiler.StringInfo>>;
                data.AddRange(tables);
        }
        public void AddCommandHandlers(params (string,DialogueRunner.CommandHandler)[] handler)
        {
            var key = COMMAND;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,DialogueRunner.CommandHandler)>();

            var data = _data[key] as List<(string,DialogueRunner.CommandHandler)>;
                data.AddRange(handler);
        }
        public void AddCommandHandlers(params (string,DialogueRunner.BlockingCommandHandler)[] handler)
        {
            var key = BLOKING_COMMAND;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,DialogueRunner.BlockingCommandHandler)>();

            var data = _data[key] as List<(string,DialogueRunner.BlockingCommandHandler)>;
                data.AddRange(handler);
        }
        public void AddFunctions(params (string,int,Action<object[]>)[] functions)
        {
            var key = FUNCTION;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,int,Action<object[]>)>();

            var data = _data[key] as List<(string,int,Action<object[]>)>;
                data.AddRange(functions);
        }
        public void AddFunctions(params (string,int,Func<object[],object>)[] functions)
        {
            var key = RETURNING_FUNCTION;
            if(!_data.ContainsKey(key))
                _data[key] = new List<(string,int,Func<object[],object>)>();

            var data = _data[key] as List<(string,int,Func<object[],object>)>;
                data.AddRange(functions);
        }
        // public void RemoveCommands(params string[] names)
        // {
        //     var key = REMOVE_COMMAND;
        //     if(!_data.ContainsKey(key))
        //         _data[key] = new List<string>();

        //     var data = _data[key] as List<string>;
        //         data.AddRange(names);
        // }
        // public void RemoveFunctions(params string[] names)
        // {
        //     var key = REMOVE_FUNCTIONS;
        //     if(!_data.ContainsKey(key))
        //         _data[key] = new List<string>();

        //     var data = _data[key] as List<string>;
        //         data.AddRange(names);
        // }
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
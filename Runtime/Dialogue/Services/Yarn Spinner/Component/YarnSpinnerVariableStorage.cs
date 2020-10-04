#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Evesoft.Dialogue.YarnSpinner.Component
{
    [HideMonoScript]
    [DisallowMultipleComponent]
    internal class YarnSpinnerVariableStorage : VariableStorageBehaviour
    {
        #region private
        private IDictionary<string,Yarn.Value> _variables;
        private IDictionary<string,object> _defaultVariables;
        #endregion

        #region property
        #if UNITY_EDITOR
        internal IDictionary<string,string> viewVariables = new Dictionary<string,string>();
        #endif
        #endregion

        #region VariableStorageBehaviour
        public override void SetValue(string variableName, Value value)
        {
            variableName = string.Format("${0}",variableName);
            _variables[variableName] = new Yarn.Value(value);

            #if UNITY_EDITOR
            viewVariables[variableName] = value.AsString;
            #endif
        }
        public override Value GetValue(string variableName)
        {
            if (!_variables.ContainsKey(variableName))
                return Yarn.Value.NULL;
            
            return _variables[variableName];
        }     
        public override void ResetToDefaults()
        {
            Clear();

            if(!_defaultVariables.IsNullOrEmpty())
                foreach (var variable in _defaultVariables)
                    SetValue(variable.Key,new Yarn.Value(variable.Value));
        }
        public override void Clear()
        {
            _variables?.Clear();
        }       
        #endregion

        #region methods
        public void SetDefaultVariable(IDictionary<string,object> defaultVariables)
        {
            _defaultVariables = defaultVariables;
            ResetToDefaults();
        }
        #endregion

        #region constructor
        public YarnSpinnerVariableStorage()
        {
            _variables = new Dictionary<string,Yarn.Value>();
        }
        #endregion
    }
}
#endif
#if ODIN_INSPECTOR && YARN_SPINNER
using System.Collections.Generic;
using Yarn;
using Yarn.Unity;
using Sirenix.OdinInspector;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinnerVariableStorage : VariableStorageBehaviour
    {
        #region private
        [ShowInInspector,HideLabel]
        private IDictionary<string,Yarn.Value> _variables;
        private IDictionary<string,object> _defaultVariables;
        #endregion

        #region VariableStorageBehaviour
        public override void SetValue(string variableName, Value value)
        {
            variableName = string.Format("${0}",variableName);
            _variables[variableName] = new Yarn.Value(value);
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

        #region constructor
        public YarnSpinnerVariableStorage(IDictionary<string,object> defaultVariables = null)
        {
            _variables          = new Dictionary<string, Yarn.Value>();
            _defaultVariables   = defaultVariables;
            ResetToDefaults();
        }
        #endregion
    }
}
#endif
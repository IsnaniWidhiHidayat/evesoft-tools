using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueConfigFactory
    {
        #if YARN_SPINNER
        public static iDialogueConfig CreateYarnSpinnerConfig(iDialogueUI ui,IDictionary<string,object> defaultVariables = null,YarnProgram[] scripts = null,(string,int,Yarn.Function)[] functions = null,(string,int,Yarn.ReturningFunction)[] returningFunctions = null)
        {
            var config =  new YarnSpinner.YarnSpinnerConfig();
                config.SetUI(ui);

                if(!defaultVariables.IsNullOrEmpty())
                    config.SetDefaultValue(defaultVariables);

                if(!scripts.IsNullOrEmpty())
                    config.AddScripts(scripts);

                if(!functions.IsNullOrEmpty())
                    config.AddFunctions(functions);

                if(!returningFunctions.IsNullOrEmpty())
                    config.AddFunctions(returningFunctions);

            return config;
        }   
        #endif 
    }
}
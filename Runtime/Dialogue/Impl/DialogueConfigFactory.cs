using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueConfigFactory
    {
        #if YARN_SPINNER
        public static iDialogueConfig CreateYarnSpinnerConfig(iDialogueUI ui,IDictionary<string,object> defaultVariables,params YarnProgram[] scripts)
        {
            var config =  new YarnSpinner.YarnSpinnerConfig();
                config.SetUI(ui);
                config.SetDefaultValue(defaultVariables);
                config.AddScripts(scripts);

            return config;
        }   
        #endif 
    }
}
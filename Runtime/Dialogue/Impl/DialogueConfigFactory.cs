using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueConfigFactory
    {
        #if YARN_SPINNER
        public static IDialogueConfig CreateYarnSpinnerConfig(IDialogueUI ui,YarnProgram[] scripts,IDictionary<string,object> defaultVariables = null)
        {
            var config =  new YarnSpinner.YarnSpinnerConfig();
            if(!ui.IsNull())
                config.SetUI(ui);

            if(!defaultVariables.IsNullOrEmpty())
                config.SetDefaultValue(defaultVariables);

            if(!scripts.IsNullOrEmpty())
                config.SetScripts(scripts);

            return config;
        }   
        #endif 
    }
}
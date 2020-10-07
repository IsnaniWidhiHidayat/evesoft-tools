using System.Collections.Generic;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueConfigFactory
    {
        #if YARN_SPINNER
        public static iDialogueConfig CreateYarnSpinnerConfig(iDialogueUI ui,IDictionary<string,object> defaultVariables = null,YarnProgram[] scripts = null,(string,int,Yarn.Function)[] functions = null,(string,int,Yarn.ReturningFunction)[] returningFunctions = null,(string,Yarn.Unity.DialogueRunner.CommandHandler)[] commands = null,(string,Yarn.Unity.DialogueRunner.BlockingCommandHandler)[] blokingCommands = null)
        {
            var config =  new YarnSpinner.YarnSpinnerConfig();
                config.SetUI(ui);

                if(!defaultVariables.IsNullOrEmpty())
                    config.SetDefaultValue(defaultVariables);

                var data = DialogueDataFactory.CreateYarnSpinnerData();
        
                if(!scripts.IsNullOrEmpty())
                    data.AddScripts(scripts);

                if(!functions.IsNullOrEmpty())
                    data.AddFunctions(functions);

                if(!returningFunctions.IsNullOrEmpty())
                    data.AddFunctions(returningFunctions);

                if(!commands.IsNullOrEmpty())
                    data.AddCommandHandlers(commands);

                if(!blokingCommands.IsNullOrEmpty())
                    data.AddCommandHandlers(blokingCommands);

                config.SetData(data);

            return config;
        }   
        #endif 
    }
}
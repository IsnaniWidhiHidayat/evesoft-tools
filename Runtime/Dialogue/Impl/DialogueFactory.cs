using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueFactory
    {
        private static IDictionary<DialogueType,IDialogue> dialogue = new Dictionary<DialogueType,IDialogue>();
        internal static event Action<IDialogue> onCreate;

        public static IDialogue Create(IDialogueConfig config)
        {
             if(config.IsNull())
                return null;

            var service = config.GetConfig<DialogueType>(nameof(Dialogue));

            switch(service)
            {
                #if YARN_SPINNER
                case DialogueType.YarnSpinner:
                {
                    var ui          = config.GetConfig<IDialogueUI>(YarnSpinner.YarnSpinnerConfig.UI);
                    var startAuto   = config.GetConfig<bool>(YarnSpinner.YarnSpinnerConfig.START_AUTO);
                    var startNode   = config.GetConfig<string>(YarnSpinner.YarnSpinnerConfig.START_NODE);
                    var variables   = config.GetConfig<IDictionary<string,object>>(YarnSpinner.YarnSpinnerConfig.DEFAULT_VARIABLES_STORAGE);
                    var scripts     = config.GetConfig<YarnProgram[]>(YarnSpinner.YarnSpinnerConfig.SCRIPTS);
                    var yarnSpinner = default(YarnSpinner.YarnSpinner);
                    var isnew       = false;
                    
                    if(dialogue.ContainsKey(service))
                    {
                        yarnSpinner = dialogue[service] as YarnSpinner.YarnSpinner;
                    }
                    else
                    {
                        yarnSpinner = new YarnSpinner.YarnSpinner();
                        isnew       = true;
                    }

                    yarnSpinner.SetUI(ui);
                    yarnSpinner.SetDefaultVariables(variables);
                    yarnSpinner.SetScripts(scripts);
                    yarnSpinner.SetStartNode(startNode);
                    yarnSpinner.SetStartAuto(startAuto);
                    
                    dialogue[service] = yarnSpinner;

                    if(isnew)
                    {
                        onCreate?.Invoke(dialogue[service]);
                    }

                    return dialogue[service];
                } 
                #endif
                   
                default:
                {
                    "Dialogue Service Unavailable".LogError();
                    return null;
                }
            }
        }
        public static IDialogue Get(DialogueType type)
        {
            if(dialogue.ContainsKey(type))
                return dialogue[type];

            return null;
        }
        public static async Task<IDialogue> GetAsync(DialogueType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }              
    }
}
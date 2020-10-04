using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evesoft.Dialogue
{
    public static class DialogueFactory
    {
        private static IDictionary<DialogueType,iDialogue> dialogue = new Dictionary<DialogueType,iDialogue>();

        public static iDialogue Create(iDialogueConfig config)
        {
             if(config.IsNull())
                return null;

            var service = config.GetConfig<DialogueType>(nameof(Dialogue));
            if(dialogue.ContainsKey(service))
                return dialogue[service];

            switch(service)
            {
                #if YARN_SPINNER
                case DialogueType.YarnSpinner:
                {
                    return dialogue[service] = new YarnSpinner.YarnSpinner(config);
                } 
                #endif
                   
                default:
                {
                    "Dialogue Service Unavailable".LogError();
                    return null;
                }
            }
        }
        public static iDialogue Get(DialogueType type)
        {
            if(dialogue.ContainsKey(type))
                return dialogue[type];

            return null;
        }
        public static async Task<iDialogue> GetAsync(DialogueType type)
        {
            await new WaitUntil(()=> !Get(type).IsNull());
            return Get(type);
        }              
    }
}
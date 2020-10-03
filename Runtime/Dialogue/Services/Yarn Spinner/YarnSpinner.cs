#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using UnityEngine;
using Yarn;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinner : iDialogue
    {
        #region private
        private iDialogueUI _dialogueUI;
        private DialogueRunner _dialogeRunner;
        private GameObject gameObject;
        #endregion       

        #region iDialogue
        public string currentNode => _dialogeRunner?.CurrentNodeName;

        public event Action<iDialogue, string> onNodeStart;
        public event Action<iDialogue, string> onNodeComplete;

        public bool IsNodeExist(string name)
        {
            return _dialogeRunner.NodeExists(name);
        }
        public void Add(iDialogueData data)
        {
            //Add Yarn Program
            var yarnProgram = data.GetValue<YarnProgram>(nameof(YarnProgram));
            if(!yarnProgram.IsNull())
                _dialogeRunner.Add(yarnProgram);

            //Add Command Handler
            (var commandName, var commandHandler)  = data.GetValue<(string,DialogueRunner.CommandHandler)>(nameof(DialogueRunner.CommandHandler));
            if(!commandName.IsNullOrEmpty() && !commandHandler.IsNull())
                _dialogeRunner.AddCommandHandler(commandName,commandHandler);

            //Add Function
            (var name,var paramCount,var function) = data.GetValue<(string,int,Function)>(nameof(Function));
            if(!name.IsNullOrEmpty() && !function.IsNull())
                _dialogeRunner.AddFunction(name,paramCount,function);

            //Add Returning Functions;
            var returningFunction = default(ReturningFunction);
            (name,paramCount,returningFunction) = data.GetValue<(string,int,ReturningFunction)>(nameof(ReturningFunction));
            if(!name.IsNullOrEmpty() && !returningFunction.IsNull())
                _dialogeRunner.AddFunction(name,paramCount,returningFunction);
        }      
        public void Remove(iDialogueData data)
        {
            //Remove Command Handler
            var name = data.GetValue<string>(nameof(DialogueRunner.CommandHandler));
            if(!name.IsNullOrEmpty())
                _dialogeRunner.RemoveCommandHandler(name);

            //Remove Function
            name = data.GetValue<string>(nameof(Function));
            if(!name.IsNullOrEmpty())
                _dialogeRunner.RemoveFunction(name);
        }
        public void StartDialogue(string node = null)
        {
            _dialogeRunner?.StartDialogue(node);
        }      
        public void ResetDialogue()
        {
            _dialogeRunner?.ResetDialogue();
        }
        public void StopDialogue()
        {
            _dialogeRunner?.Stop();
        }
        public void Clean()
        {
           _dialogeRunner?.Clear();
        } 
        #endregion

        #region constructor
        public YarnSpinner(iDialogueConfig config = null)
        {
            if(!config.IsNull())
            {
                _dialogueUI = config.GetConfig<iDialogueUI>(nameof(iDialogueUI));
                _dialogueUI.IsNull().Log();
            }

            gameObject                      = new GameObject(nameof(YarnSpinner));
            _dialogeRunner                  = gameObject.AddComponent<DialogueRunner>();
            _dialogeRunner.variableStorage  = new YarnSpinnerVariableStorage();
            _dialogeRunner.dialogueUI       = _dialogueUI as DialogueUIBehaviour;
        }
        #endregion
    }
}
#endif
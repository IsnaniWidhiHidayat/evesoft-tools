#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinner : iDialogue
    {
        #region private
        private DialogueRunner _dialogeRunner;
        #endregion       

        #region property
        public DialogueRunner dialogeRunner => _dialogeRunner;

        #if UNITY_EDITOR
        private IDictionary<string,string> _functions;
        public IDictionary<string,string> functions
        {
            get
            {
                return _functions;
            }
        }
        public IDictionary<string,string> variables 
        {
            get
            {
                if(_dialogeRunner.IsNull())
                    return null;

                var storage = _dialogeRunner.variableStorage as Component.YarnSpinnerVariableStorage;
                if(storage.IsNull())
                    return null;
                
                return storage.viewVariables;
            }
        }
        #endif
        #endregion

        #region editor methods
        private void AddRegisteredFunctions(byte type,string name,int paramCount)
        {
            #if UNITY_EDITOR
            if(_functions.IsNull())
                _functions = new Dictionary<string,string>();

            _functions[name] = string.Format("{2}{0}(params obj[{1}] param)",name,paramCount,type ==0 ?"" : "object ");
            #endif
        }
        private void RemoveRegisteredFunctions(string name)
        {
            #if UNITY_EDITOR
            if(_functions.IsNullOrEmpty())
                return;

            _functions.Remove(name);
            #endif
        }
        #endregion

        #region iDialogue
        public string currentNode => _dialogeRunner?.CurrentNodeName;

        public event Action<iDialogue, string> onDialogueStart;
        public event Action<iDialogue, string> onDialogueEnd;
        public event Action<iDialogue> onDialogueComplete;

        public bool IsNodeExist(string name)
        {
            return _dialogeRunner.NodeExists(name);
        }
        public void Add(iDialogueData data)
        {
            //Add Yarn Program
            var scripts = data.GetValue<IList<YarnProgram>>(YarnSpinnerData.SCRIPT);
            if(!scripts.IsNullOrEmpty())
            {
                foreach (var script in scripts)
                    _dialogeRunner.Add(script);
            }

            //Add stringTable
            var tables = data.GetValue<IList<IDictionary<string,Yarn.Compiler.StringInfo>>>(YarnSpinnerData.STRING_TABLE);
            if(!tables.IsNullOrEmpty())
            {
                foreach (var table in tables)
                    _dialogeRunner.AddStringTable(table);
            }
                
            //Add Command Handler
            var commandHandlers = data.GetValue<IList<(string,DialogueRunner.CommandHandler)>>(YarnSpinnerData.COMMAND_HANDLER);
            if(!commandHandlers.IsNullOrEmpty())
            {
                foreach (var command in commandHandlers)
                {
                    (var name,var handler) = command;
                    _dialogeRunner.AddCommandHandler(name,handler);
                }
            }
                
            //Add Function
            var functions = data.GetValue<IList<(string,int,Yarn.Function)>>(YarnSpinnerData.FUNCTION);
            if(!functions.IsNullOrEmpty())
            { 
                foreach (var function in functions)
                {
                    (var name,var paramCount,var func) = function;
                    _dialogeRunner.AddFunction(name,paramCount,func);

                    AddRegisteredFunctions(0,name,paramCount);
                }
            }
               
            //Add Returning Functions;
            var returningFunction = data.GetValue<IList<(string,int,Yarn.ReturningFunction)>>(YarnSpinnerData.RETURNING_FUNCTION);
            if(!returningFunction.IsNullOrEmpty())
            {
               foreach (var function in returningFunction)
                {
                    (var name,var paramCount,var func) = function;
                    _dialogeRunner.AddFunction(name,paramCount,func);

                    AddRegisteredFunctions(1,name,paramCount);
                } 
            }
        }      
        public void Remove(iDialogueData data)
        {
            //Remove Command Handler
            var names = data.GetValue<IList<string>>(YarnSpinnerData.REMOVE_COMMAND);
            if(!names.IsNullOrEmpty())
            {
                foreach (var name in names)
                    _dialogeRunner.RemoveCommandHandler(name);
            }
                
            //Remove Function
            names = data.GetValue<IList<string>>(YarnSpinnerData.REMOVE_FUNCTIONS);
            if(!names.IsNullOrEmpty())
            {
                foreach (var name in names)
                {
                    _dialogeRunner.RemoveFunction(name);
                    RemoveRegisteredFunctions(name);
                }
            }
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
        internal YarnSpinner(iDialogueConfig config)
        { 
            var attachTo  = config.GetConfig<GameObject>(YarnSpinnerConfig.ATTACH_TO);
            var ui        = config.GetConfig<iDialogueUI>(YarnSpinnerConfig.UI);
            var startAuto = config.GetConfig<bool>(YarnSpinnerConfig.START_AUTO);
            var startNode = config.GetConfig<string>(YarnSpinnerConfig.START_NODE);
            var scripts   = config.GetConfig<IList<YarnProgram>>(YarnSpinnerConfig.SCRIPTS);
            var defaultVariables = config.GetConfig<IDictionary<string,object>>(YarnSpinnerConfig.DEFAULT_VARIABLES_STORAGE);
           
            //Set dialogue Runner
            _dialogeRunner  = (attachTo.IsNull()? new GameObject(nameof(YarnSpinner)) : attachTo).AddComponent<DialogueRunner>();
            
            //Set Events
            SetEvents(_dialogeRunner);

            //Set Storage
            var storage = _dialogeRunner.gameObject.AddComponent<Component.YarnSpinnerVariableStorage>();
            storage.SetDefaultVariable(defaultVariables);
            _dialogeRunner.variableStorage  = storage;


            //Set UI
            var UI = _dialogeRunner.gameObject.AddComponent<Component.YarnSpinnerUI>();
            UI.SetUI(ui);
            _dialogeRunner.dialogueUI  = UI;
            
            //Set node
            _dialogeRunner.startAutomatically = startAuto;
            _dialogeRunner.startNode          = startNode;

            //Set Scripts
            if(!scripts.IsNullOrEmpty())
            {
                _dialogeRunner.yarnScripts = new YarnProgram[scripts.Count];
                scripts.CopyTo(_dialogeRunner.yarnScripts,0);
            }
            else
            {
                _dialogeRunner.yarnScripts = new YarnProgram[0];
            }

            //Hide Game Object
            _dialogeRunner.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }
        #endregion

        #region methods
        private void SetEvents(DialogueRunner dialogue)
        {
            RemoveEvents(dialogue);

            if(dialogue.IsNull())
                return;

            if(dialogue.onNodeStart.IsNull())
                dialogue.onNodeStart = new DialogueRunner.StringUnityEvent();

            if(dialogue.onNodeComplete.IsNull())
                dialogue.onNodeComplete = new DialogueRunner.StringUnityEvent();

            if(dialogue.onDialogueComplete.IsNull())
                dialogue.onDialogueComplete = new UnityEngine.Events.UnityEvent();

            dialogue.onNodeStart.AddListener(OnNodeStart);
            dialogue.onNodeComplete.AddListener(OnNodeComplete);
            dialogue.onDialogueComplete.AddListener(OnDialogComplete);
        }
        private void RemoveEvents(DialogueRunner dialogue)
        {
            if(dialogue.IsNull())
                return;

            dialogue.onNodeStart?.RemoveListener(OnNodeStart);
            dialogue.onNodeComplete?.RemoveListener(OnNodeComplete);
            dialogue.onDialogueComplete?.RemoveListener(OnDialogComplete);
        }    
        #endregion

        #region callbacks
        private void OnDestroy()
        {
            RemoveEvents(_dialogeRunner);
        }
        private void OnNodeStart(string node)
        {
            onDialogueStart?.Invoke(this,node);
            $"{nameof(OnNodeStart)} {node}".Log();
        }
        private void OnNodeComplete(string node)
        {
            onDialogueEnd?.Invoke(this,node);
            $"{nameof(OnNodeComplete)} {node}".Log();
        } 
        private void OnDialogComplete()
        {
            onDialogueComplete?.Invoke(this);
            $"{nameof(OnDialogComplete)}".Log();
        }
        #endregion
    }
}
#endif
#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace Evesoft.Dialogue.YarnSpinner
{
    public class YarnSpinner : IDialogue
    {
        #region private
        private DialogueRunner _dialogeRunner;
        private Component.YarnSpinnerVariableStorage _storage;
        private Component.YarnSpinnerUI _ui;
        #endregion       

        #region property
        internal DialogueRunner dialogeRunner => _dialogeRunner;
        internal Component.YarnSpinnerVariableStorage storage => _storage;
        internal Component.YarnSpinnerUI ui => _ui;

        #if UNITY_EDITOR
        public IDictionary<string,string> functions{get;private set;}
        public IDictionary<string,string> commands{get;private set;}
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
        internal void AddEditorRegisteredFunctions(byte type,string name,int paramCount)
        {
            #if UNITY_EDITOR
            if(functions.IsNull())
                functions = new Dictionary<string,string>();

            functions[name] = string.Format("{2}{0}(params object[{1}] param)",name,paramCount,type == 0 ?"" : "object ");
            #endif
        }
        internal void RemoveEditorRegisteredFunctions(string name)
        {
            #if UNITY_EDITOR
            if(functions.IsNullOrEmpty())
                return;

            functions.Remove(name);
            #endif
        }
        internal void AddEditorRegisteredCommand(byte type,string name)
        {
            #if UNITY_EDITOR
            if(commands.IsNull())
                commands = new Dictionary<string,string>();

            commands[name] = string.Format("{0}(params string[] param{1})",name,type == 0?"" : ", Action onComplete");
            #endif
        }
        internal void RemoveEditorRegisteredCommand(string name)
        {
            #if UNITY_EDITOR
            if(commands.IsNullOrEmpty())
                return;

            commands.Remove(name);
            #endif
        }
        #endregion

        #region iDialogue
        public string currentNode => _dialogeRunner?.CurrentNodeName;

        public event Action<IDialogue, string> onDialogueStart;
        public event Action<IDialogue, string> onDialogueEnd;
        public event Action<IDialogue> onDialogueComplete;

        public bool IsNodeExist(string name)
        {
            return _dialogeRunner.NodeExists(name);
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
        public void Clear()
        {
           _dialogeRunner?.Clear();
        } 
        #endregion

        #region constructor
        internal YarnSpinner()
        { 
            //Set dialogue Runner
            _dialogeRunner  = new GameObject(nameof(YarnSpinner)).AddComponent<DialogueRunner>();
            _dialogeRunner.variableStorage = _storage = _dialogeRunner.gameObject.AddComponent<Component.YarnSpinnerVariableStorage>();
            _dialogeRunner.dialogueUI      = _ui      = _dialogeRunner.gameObject.AddComponent<Component.YarnSpinnerUI>();

            //Set Events
            SetEvents(_dialogeRunner);

            _dialogeRunner.gameObject.hideFlags = HideFlags.HideInHierarchy;
            GameObject.DontDestroyOnLoad(_dialogeRunner.gameObject);
        }
        #endregion

        #region methods
        internal void SetUI(IDialogueUI ui){
            _ui.SetUI(ui);
        }
        internal void SetDefaultVariables(IDictionary<string,object> variables)
        {
            _storage.SetDefaultVariable(variables);
        }
        internal void SetStartNode(string startNode)
        {
            _dialogeRunner.startNode  = startNode;
        }
        internal void SetStartAuto(bool startAuto)
        {
            _dialogeRunner.startAutomatically = startAuto;
        }
        internal void SetScripts(YarnProgram[] scripts)
        {
            _dialogeRunner.yarnScripts = scripts;
        }

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
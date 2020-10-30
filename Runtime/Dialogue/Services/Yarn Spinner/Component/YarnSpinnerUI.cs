#if ODIN_INSPECTOR && YARN_SPINNER
using System;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;

namespace Evesoft.Dialogue.YarnSpinner.Component
{
    [DisallowMultipleComponent]
    internal class YarnSpinnerUI : DialogueUIBehaviour
    {
        #region private
        private IDialogueUI _ui;
        private Queue<IDialogueOptions> _poolOptions;
        private IList<IDialogueOptions> _options;
        private YarnSpinnerDialogueLine _line;
        #endregion

        #region methods
        public void SetUI(IDialogueUI ui)
        {
            this._ui = ui;
        }
        #endregion

        #region DialogueUIBehaviour
        public override void DialogueStarted()
        {
            _ui?.OnDialogueStart();         
        }
        public override void DialogueComplete()
        {
            _ui?.OnDialogueEnd();
        }
        public override void RunOptions (Yarn.OptionSet optionSet, ILineLocalisationProvider localisationProvider, Action<int> onOptionSelected) {
           
            if(_options.IsNull())
                _options = new List<IDialogueOptions>();

            if(_poolOptions.IsNull())
                _poolOptions = new Queue<IDialogueOptions>();

            //pool
            for (int i = 0; i < optionSet.Options.Length; i++)
            {
                var option       = optionSet.Options[i];
                var id           = option.ID;
                var localizeText = localisationProvider.GetLocalisedTextForLine(option.Line);
                var text         = localizeText;

                if(_options.Count-1 < i)
                {
                    var newValue = _poolOptions.Count > 0? _poolOptions.Dequeue() : new YarnSpinnerDialogueOptions(id,text,localizeText);
                    _options.Add(newValue);
                }
                   
                var current = _options[i] as YarnSpinnerDialogueOptions;
                current.id  = id;
                current.text = text;
                current.localizeText = localizeText;
                current.RemoveAllListener();
                current.AddListener(val =>
                {
                    onOptionSelected?.Invoke(val.id);
                    _ui?.OnOptionsSelected(val.id);
                    _ui?.OnOptionsEnd();
                });
            }

            if(_options.Count > optionSet.Options.Length)
            {
                var count = _options.Count - optionSet.Options.Length;
                for (int i = 0; i < count; i++)
                {
                    var option = _options.Last() as YarnSpinnerDialogueOptions;
                        option.RemoveAllListener();

                    _poolOptions.Enqueue(option);
                    _options.Remove(option);
                }
            }

            _ui?.OnOptionsStart(_options);
        }
        public override Yarn.Dialogue.HandlerExecutionType RunLine(Yarn.Line line, ILineLocalisationProvider localisationProvider, System.Action onLineComplete)
        {
            var text = localisationProvider.GetLocalisedTextForLine(line);
            if(text.IsNullOrEmpty())
            {
                text = line.ID;
                $"Line {line.ID} doesn't have any localised text".Log();
            }
            
            if(_line.IsNull())
                _line = new YarnSpinnerDialogueLine();

            _line.text = text;
            _line.RemoveAllListener();
            _line.AddListener(onLineComplete);
            _line.AddListener(_ui.OnLineEnd);
            _ui?.OnLineStart(_line);

            return Yarn.Dialogue.HandlerExecutionType.PauseExecution;
        }
        public override Yarn.Dialogue.HandlerExecutionType RunCommand(Yarn.Command command, System.Action onCommandComplete) 
        {
            _ui?.OnCommand(command.Text);
            return Yarn.Dialogue.HandlerExecutionType.ContinueExecution;
        }        
        #endregion
        
        #region constructor
        public YarnSpinnerUI(){}
        public YarnSpinnerUI(IDialogueUI ui = null)
        {
            this._ui = ui;
        }
        #endregion
    }
}
#endif
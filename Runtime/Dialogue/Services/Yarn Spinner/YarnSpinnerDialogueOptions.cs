#if ODIN_INSPECTOR && YARN_SPINNER
using System;

namespace Evesoft.Dialogue.YarnSpinner
{
    internal class YarnSpinnerDialogueOptions : IDialogueOptions
    {
        #region iDialogueOptions
        public int id {get;set;}
        public string text {get;set;}
        public string localizeText {get;set;}

        public event Action<IDialogueOptions> onClick;

        public void Select()
        {
            onClick?.Invoke(this);
        }
        public void Dispose()
        {
            onClick = null;
        }
        #endregion

        #region methods
        public void RemoveAllListener()
        {
            onClick = null;
        }
        public void AddListener(Action<IDialogueOptions> listener)
        {
            onClick += listener;
        }
        public void RemoveListener(Action<IDialogueOptions> listener)
        {
            onClick -= listener;
        }
        #endregion

        #region constructor
        internal YarnSpinnerDialogueOptions(int id,string text,string localizeText)
        {
            this.id     = id;
            this.text   = text;
            this.localizeText = localizeText;
        }
        #endregion
    }
}
#endif
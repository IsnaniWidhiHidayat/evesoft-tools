#if ODIN_INSPECTOR && YARN_SPINNER
using System;

namespace Evesoft.Dialogue.YarnSpinner
{
    internal class YarnSpinnerDialogueLine : IDialogueLine ,IDisposable
    {
        #region private
        private string _text;
        private event Action _listeners;
        #endregion

        #region methods
        public void AddListener(Action listener)
        {
            _listeners += listener;
        }
        public void RemoveListener(Action listener)
        {
            _listeners -= listener;
        }
        public void RemoveAllListener()
        {
            _listeners = null;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
           RemoveAllListener();
        }
        #endregion

        #region iDialogueLine
        public string text {get => _text; internal set => _text = value;}
        public void Next()
        {
            _listeners?.Invoke();
        }
        #endregion

        #region constructor
        public YarnSpinnerDialogueLine(){}
        public YarnSpinnerDialogueLine(string text)
        {
            _text = text;
            _listeners = default(Action);
        }
        #endregion
    }
}
#endif
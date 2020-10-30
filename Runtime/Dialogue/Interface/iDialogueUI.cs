using System;
using System.Collections.Generic;

namespace Evesoft.Dialogue
{
    public interface IDialogueUI : IDisposable
    {
        event Action onDialogueStart;
        event Action onDialogueEnd;
        event Action onLineStart;
        event Action onLineFinishDisplaying;
        event Action onLineEnd;
        event Action onOptionsStart;
        event Action onOptionSelected;
        event Action onOptionsSelected;
        event Action onOptionsEnd;
        

        void OnDialogueStart();
        void OnDialogueEnd();
        void OnLineStart(IDialogueLine line);
        void OnLineEnd();
        void OnOptionsStart(IList<IDialogueOptions> options);
        void OnOptionsSelected(int ID);
        void OnOptionsEnd();
        void OnCommand(string command);
    }
}
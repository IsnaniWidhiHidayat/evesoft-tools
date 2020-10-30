using System;
using System.Collections.Generic;

namespace Evesoft.Dialogue
{
    public interface IDialogueUI : IDisposable
    {
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
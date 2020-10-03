using System;

namespace Evesoft.Dialogue
{
    public interface iDialogueUI
    {
        event Action<iDialogue> onDialogueStart;
        event Action<iDialogue> onDialogueEnd;
        event Action<iDialogue> onDialogueComplete;
        event Action<iDialogue,string> onLineStart;
        event Action<iDialogue,string> onLineUpdate;
        event Action<iDialogue,string> onLineEnd;
        event Action<iDialogue,string> onLineFinishDisplaying;
        event Action<iDialogue> onOptionsStart;
        event Action<iDialogue,int> onOptionsSelected;
        event Action<iDialogue> onOptionsEnd;

        void Next();
    }
}
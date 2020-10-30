using System;
using System.Collections.Generic;

namespace Evesoft.Dialogue
{
    public interface IDialogue
    {
        event Action<IDialogue,string> onDialogueStart;
        event Action<IDialogue,string> onDialogueEnd;
        event Action<IDialogue> onDialogueComplete;
        
        string currentNode{get;}

        bool IsNodeExist(string name);
        void StartDialogue(string node);
        void ResetDialogue();
        void StopDialogue();
        void Clean();
        void Add(IDialogueData data);
        void Remove(IDialogueData data);
    }
}
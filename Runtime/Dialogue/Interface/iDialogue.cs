using System;
using System.Collections.Generic;

namespace Evesoft.Dialogue
{
    public interface iDialogue
    {
        event Action<iDialogue,string> onDialogueStart;
        event Action<iDialogue,string> onDialogueEnd;
        event Action<iDialogue> onDialogueComplete;
        
        string currentNode{get;}

        bool IsNodeExist(string name);
        void StartDialogue(string node);
        void ResetDialogue();
        void StopDialogue();
        void Clean();
        void Add(iDialogueData data);
        void Remove(iDialogueData data);
    }
}
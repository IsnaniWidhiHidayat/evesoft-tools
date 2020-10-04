using System;
namespace Evesoft.Dialogue
{
    public interface iDialogueOptions : IDisposable
    {
        event Action<iDialogueOptions> onClick;

        int id {get;}
        string text {get;}
        string localizeText{get;}
        
        void Select();
    }
}
using System;
namespace Evesoft.Dialogue
{
    public interface IDialogueOptions : IDisposable
    {
        event Action<IDialogueOptions> onClick;

        int id {get;}
        string text {get;}
        string localizeText{get;}
        
        void Select();
    }
}
namespace Evesoft.Dialogue
{
    public interface IDialogueConfig
    {
        T GetConfig<T>(string key); 
    }
}
namespace Evesoft.Dialogue
{
    public interface iDialogueConfig
    {
        T GetConfig<T>(string key); 
    }
}
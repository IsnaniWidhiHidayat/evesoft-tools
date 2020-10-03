namespace Evesoft.Dialogue
{
    public interface iDialogueData
    {
        T GetValue<T>(string key);
    }
}
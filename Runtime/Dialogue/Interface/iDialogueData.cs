namespace Evesoft.Dialogue
{
    public interface IDialogueData
    {
        T GetValue<T>(string key);
    }
}
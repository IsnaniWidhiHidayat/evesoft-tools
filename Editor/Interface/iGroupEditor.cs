namespace Evesoft.Editor
{
    public interface iGroupEditor
    {
        string name{get;}
        void Refresh();
        void OnScriptReloaded();
        void OnWindowClicked();
    }
}
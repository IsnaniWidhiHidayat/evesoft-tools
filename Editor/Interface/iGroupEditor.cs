#if ODIN_INSPECTOR 
namespace Evesoft.Editor
{
    public interface iGroupEditor : iRefresh
    {
        string name{get;}
        void OnScriptReloaded();
        void OnWindowClicked();
        void OnGUI();
    }
}
#endif
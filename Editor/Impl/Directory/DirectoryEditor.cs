using Sirenix.OdinInspector;

namespace Evesoft.Editor.Directory
{
    [HideReferenceObjectPicker]
    public class DirectoryEditor : iGroupEditor
    {
        [EnableGUI,ShowInInspector,InlineButton(nameof(OpenDir),"Open")]private string project => DirectoryUtility.projectLocation;
        [EnableGUI,ShowInInspector,InlineButton(nameof(OpenPersistentDirectory),"Open")]private string persistent => DirectoryUtility.persistentLocation;
        [EnableGUI,ShowInInspector,InlineButton(nameof(OpenCacheDirectory),"Open")]private string cache => DirectoryUtility.cacheLocation;
        [EnableGUI,ShowInInspector,InlineButton(nameof(OpenConsoleDir),"Open")]private string console => DirectoryUtility.consoleLocation;
        [EnableGUI,ShowInInspector,InlineButton(nameof(OpenStreamAssetsDir),"Open")]private string streamAsset => DirectoryUtility.streamAssetLocation;

        private void OpenDir()
        {
           DirectoryUtility.OpenDir();
        }
        private void OpenPersistentDirectory()
        {
           DirectoryUtility.OpenPersistentDirectory();
        }
        private void OpenCacheDirectory()
        {
            DirectoryUtility.OpenCacheDirectory();
        }    
        private void OpenConsoleDir()
        {
            DirectoryUtility.OpenConsoleDir();
        }
        private void OpenStreamAssetsDir()
        {
           DirectoryUtility.OpenStreamAssetsDir();
        }
    


        #region iGroupEditor
        public string name => "Dir";
        public void Refresh()
        {
            
        }
        
        public void OnScriptReloaded()
        {
            
        }
        public void OnWindowClicked()
        {
            
        }
        public void OnGUI()
        {
           
        }
        #endregion
    }
}
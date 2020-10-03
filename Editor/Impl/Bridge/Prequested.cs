#if ODIN_INSPECTOR 
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace Evesoft.Editor.Bridge
{
    [HideReferenceObjectPicker]
    public class Prequested:iRefresh
    {
        [DisplayAsString,HideLabel,HorizontalGroup,SuffixLabel("$"+nameof(getStatus),true)]
        public string name;
        private string url;
        private string namespaceclass;
        private BuildTarget[] platforms;

        [HideInInspector]
        public bool isInstalled;

        [HideInInspector]
        public bool isPlatformSupported;

        [Button("Open",ButtonSizes.Small),HorizontalGroup(width:60)]
        public void OpenUrl()
        {
            if(url.IsNullOrEmpty())
                return;

            Application.OpenURL(url);
        }
        private string getStatus()
        {
            return isInstalled?"installed":"Not installed";
        }

        #region iRefresh
        public void Refresh()
        {
            isInstalled = !namespaceclass.IsNullOrEmpty()? NamespaceUtility.IsNamespaceExists(namespaceclass) : true;
            
            if(!isInstalled)
                isInstalled = !namespaceclass.IsNullOrEmpty()? TypeUtility.IsTypeExist(namespaceclass) : true;

            isPlatformSupported = platforms.IsNullOrEmpty()? true : ArrayUtility.Contains(platforms,EditorUserBuildSettings.activeBuildTarget);
        }
        #endregion

        #region constructor
        public Prequested(string name,string nameSpaceOrClass,string url,params BuildTarget[] platforms)
        {
            this.name = name;
            this.url = url;
            this.platforms = platforms;
            this.namespaceclass = nameSpaceOrClass;
        }
        #endregion
    }
}


#endif
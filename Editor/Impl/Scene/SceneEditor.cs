#if ODIN_INSPECTOR 

using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Evesoft.Editor.Scene
{
    [HideReferenceObjectPicker]
    public class SceneLoader
    {
        #region Field
        [HideLabel,DisplayAsString,HorizontalGroup]
        public string name;

        [HideInInspector]
        public string path;

        [HideInInspector]
        public OpenSceneMode openMode;
        #endregion

        #region Constructor
        public SceneLoader(string name, string path, OpenSceneMode openMode = OpenSceneMode.Single)
        {
            this.name = name;
            this.path = path;
            this.openMode = openMode;
        }
        #endregion
        [Button(size: ButtonSizes.Small, Name = "Single"),HorizontalGroup(Width = 60)]
        public void LoadSingle()
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                    openMode = OpenSceneMode.Single;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        [Button(size: ButtonSizes.Small, Name = "Additive"),HorizontalGroup(Width = 60)]
        public void LoadAdditive()
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                    openMode = OpenSceneMode.Additive;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }

        [Button(size: ButtonSizes.Small),HorizontalGroup(Width = 40)]
        public void Ping()
        {
            var path    = "Assets" + this.path.Replace(Application.dataPath,string.Empty);
            var obj     = AssetDatabase.LoadMainAssetAtPath(path);
            EditorGUIUtility.PingObject(obj);
        }
    }

    [System.Serializable,HideReferenceObjectPicker]
    public class SceneEditor : iGroupEditor
    {
        #region field
        [ShowInInspector,ListDrawerSettings(HideAddButton = true, DraggableItems = false, IsReadOnly = true, ShowItemCount = false, Expanded = true,NumberOfItemsPerPage = 23)]
        private List<SceneLoader> scenes = new List<SceneLoader>();
        #endregion

        #region iGroupEditor
        public string name => "Scenes";


        public void Refresh()
        {
            scenes.Clear();

            var files = System.IO.Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                scenes.Add(new SceneLoader(Path.GetFileNameWithoutExtension(files[i]), files[i]));
            }
        }
        public void OnScriptReloaded()
        {
            Refresh();
        }
        public void OnWindowClicked()
        {
            Refresh();
        }
        public void OnGUI()
        {
            
        }
        #endregion
    } 
}

#endif
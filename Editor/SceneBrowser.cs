
using System;
using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Evesoft.Editor
{
    [Serializable]
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
        [Button(size: ButtonSizes.Small, Name = "Single"),HorizontalGroup(Width = 70)]
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

        [Button(size: ButtonSizes.Small, Name = "Additive"),HorizontalGroup(Width = 70)]
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
    }

    public class SceneBrowser : OdinEditorWindow
    {
        #region const
        const string path  = "Tools/EveSoft/Browser/Scenes";
        #endregion

        #region static
        [MenuItem(path)]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneBrowser>();
            window.Refresh();
            window.Show();
        }

        [MenuItem(path,true)]
        public static bool Validate()
        {
            var files = System.IO.Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);
            return !files.IsNullOrEmpty();
        }
        #endregion

        #region field
        [ListDrawerSettings(HideAddButton = true, DraggableItems = false, IsReadOnly = true, ShowItemCount = false, Expanded = true)]
        public List<SceneLoader> scenes = new List<SceneLoader>();
        #endregion

        #region method
        [Button(ButtonSizes.Small),GUIColor(0,1,0),PropertyOrder(-1)]
        private void Refresh()
        {
            scenes.Clear();

            var files = System.IO.Directory.GetFiles(Application.dataPath, "*.unity", SearchOption.AllDirectories);

            for (int i = 0; i < files.Length; i++)
            {
                scenes.Add(new SceneLoader(Path.GetFileNameWithoutExtension(files[i]), files[i]));
            }
        }
        #endregion
    } 
}

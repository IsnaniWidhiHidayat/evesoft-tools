#if LOCALIZE
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Evesoft.Localize
{
    [CreateAssetMenu(menuName = nameof(Evesoft) + "/" + nameof(Evesoft.Localize) + "/" + nameof(LocalizeDatabase))]
    [HideMonoScript]

    public class LocalizeDatabase : ScriptableObject
    {
        #region Static
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tools/EveSoft/Localize/Database")]
        private static void ShowTools()
        {
            var database = Evesoft.Editor.AssetDatabaseFinder.Find<LocalizeDatabase>();
            if (database != null)
                OdinEditorWindow.InspectObject(database);
        }

        [UnityEditor.MenuItem("Tools/EveSoft/Localize/Database", true)]
        private static bool ValidateTools()
        {
            var database = Editor.AssetDatabaseFinder.Find<LocalizeDatabase>();
            return database != null;
        }
#endif
        #endregion

        #region events
        public event Action<SystemLanguage> onLanguageChange;
        #endregion

        #region Field
        [PropertyOrder(-2)]
        [ShowInInspector]
        [ValueDropdown("languages")]
        [LabelWidth(60)]
        public SystemLanguage language
        {
            get
            {
                if (!languages.Contains(_language))
                {
                    _language = languages[0];
                }

                return _language;
            }

            set
            {
                //Check if language is Exist
                if (!languages.Contains(value))
                {
                    return;
                }

                //Call callback Change
                if (value != _language)
                {
                    _language = value;
                    onLanguageChange?.Invoke(value);
                }
            }
        }

        private void RemoveLanguage(SystemLanguage language)
        {
            if (languages.Count == 1)
                return;

            #if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog("Warning", string.Format("Are You Sure want delete {0} language", language.ToString()), "Yes", "No"))
            {
                languages.Remove(language);
                OnRemoveLanguage(language);
            }
            #endif    
        }
    
        [ListDrawerSettings(CustomAddFunction = "AddLanguage", CustomRemoveElementFunction = "RemoveLanguage", ShowItemCount = false, Expanded = true, HideAddButton = true, AlwaysAddDefaultValue = true)]
        [DisplayAsString]
        [PropertyOrder(-1)]
        [SerializeField]
        public List<SystemLanguage> languages = new List<SystemLanguage>() { SystemLanguage.English };


        [Button(Name = "Add")]
        [PropertyOrder(-1)]
        private void AddLanguage()
        {
            #if UNITY_EDITOR
            var source = Enum.GetValues(typeof(SystemLanguage)).Cast<SystemLanguage>();
            GenericSelector<SystemLanguage> selector = new GenericSelector<SystemLanguage>("Language", false, x => x.ToString(), source);
            selector.SelectionConfirmed += (selection) =>
            {
                var select = selection.FirstOrDefault();
                if (!languages.Contains(select))
                {
                    languages.Add(select);
                    OnAddLanguage(select);
                }
            };
            selector.ShowInPopup();
            #endif
        }


        //[ReadOnly]
        [ListDrawerSettings(IsReadOnly = true, DraggableItems = false, Expanded = true)]
        [SerializeField,HideInInspector]
        internal List<LocalizeData> localize = new List<LocalizeData>();
        #endregion

        #region Private
        [HideInInspector]
        [SerializeField]
        private SystemLanguage _language;
        #endregion

        private void OnEnable()
        {
            ClearNull();

            #if UNITY_EDITOR
            localize = Editor.AssetDatabaseFinder.Finds<LocalizeData>();
            UnityEditor.SceneManagement.EditorSceneManager.activeSceneChangedInEditMode += OnSceneLoaded;
            #endif        
        }
        private void ClearNull()
        {
            localize.RemoveAll(x => x == null);
        }

        internal void AddData(LocalizeData data)
        {
            ClearNull();

            if (!this.localize.Contains(data))
            {
                this.localize.Add(data);
            }

            this.SetObjectDirty();
        }
        internal void RemoveData(LocalizeData data)
        {
            ClearNull();

            if (this.localize.Contains(data))
            {
                this.localize.Remove(data);
            }

            this.SetObjectDirty();
        }
        public string GetLocalize(string key)
        {
            for (int i = 0; i < localize.Count; i++)
            {
                if (localize[i] != null && localize[i].key == key)
                {
                    return localize[i].value;
                }
            }

            return string.Empty;
        }
       
        private void RefreshAllLocalizeData()
        {
            for (int i = 0; i < localize.Count; i++)
            {
                localize[i].Refresh(languages);
            }
        }
        private void RefreshAllLocalizeComponent()
        {
            LocalizeLang[] langsEvent = GameObject.FindObjectsOfType<LocalizeLang>();
            for (int i = 0; i < langsEvent.Length; i++)
            {
                langsEvent[i].Refresh();
            }
        }

        private void OnAddLanguage(SystemLanguage language)
        {
            RefreshAllLocalizeData();
            RefreshAllLocalizeComponent();
        }
        private void OnRemoveLanguage(SystemLanguage language)
        {
            RefreshAllLocalizeData();
            RefreshAllLocalizeComponent();
        }
        private void OnSceneLoaded(Scene old, Scene current)
        {
            RefreshAllLocalizeComponent();
        }
    }
}
#endif
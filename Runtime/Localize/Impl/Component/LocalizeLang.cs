#if ODIN_INSPECTOR 
#if LOCALIZE
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Evesoft.Localize
{
    [System.Serializable]
    public class UnityEventLanguage : UnityEvent
    {
        [SerializeField]
        public SystemLanguage language;

        public UnityEventLanguage() : base()
        {

        }
        public UnityEventLanguage(SystemLanguage language)
        {
            this.language = language;
        }

        public override string ToString()
        {
            return string.Format("OnChange To {0}", language.ToString());
        }
    }

    [HideMonoScript]
    [AddComponentMenu(Menu.localize + "/LocalizeLang")]
    public class LocalizeLang : SerializedMonoBehaviour
    {
        #region Field
        [SerializeField]
        private LocalizeDatabase database;

        private bool ShowOnChange()
        {
            return database != null;
        }

        [Space(10)]
        [ShowIf("ShowOnChange")]
        [ListDrawerSettings(Expanded = true, IsReadOnly = true, ShowItemCount = false, ListElementLabelName = "ToString")]
        [SerializeField]
        private List<UnityEventLanguage> OnChanges = new List<UnityEventLanguage>();
        #endregion

        private void OnEnable()
        {
            if (database != null)
                database.onLanguageChange += OnLanguageChange;
        }
        private void OnDisable()
        {
            if (database != null)
                database.onLanguageChange -= OnLanguageChange;
        }
        private void Start()
        {
            if (database != null)
            {
                OnLanguageChange(database.language);
            }
        }
        private void Reset()
        {
            
#if UNITY_EDITOR
            if (database == null)
                database = Utils.AssetDatabaseFinder.Find<LocalizeDatabase>();
#endif

            Refresh();
        }
        public void Refresh()
        {
            if (database == null)
                return;

            List<SystemLanguage> languages = database.languages;
            //Delete Unused language
            int index = 0;
            while (index < OnChanges.Count)
            {
                if (!languages.Contains(OnChanges[index].language))
                {
                    OnChanges.Remove(OnChanges[index]);
                }
                else
                {
                    index++;
                }
            }

            //Add OnChange Event
            for (int i = 0; i < languages.Count; i++)
            {
                UnityEventLanguage lang = OnChanges.Find(x => x.language == languages[i]);
                if (lang == null)
                {
                    OnChanges.Add(new UnityEventLanguage(languages[i]));
                }
            }

            //Sort
            OnChanges.Sort((x, y) => x.language.CompareTo(y.language));
        }

        #region Localize Callback
        public void OnLanguageChange(SystemLanguage language)
        {
            for (int i = 0; i < OnChanges.Count; i++)
            {
                if (OnChanges[i].language == language)
                {
                    OnChanges[i].Invoke();
                }
            }
        }
        #endregion
    }
}
#endif
#endif
#if ODIN_INSPECTOR 
#if LOCALIZE
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Localize
{
    [HideMonoScript]
    [AddComponentMenu(Menu.localize + "/LocalizeChanger")]
    public class LocalizeChanger : MonoBehaviour
    {
        #region Field
        [SerializeField]
        private LocalizeDatabase database;
        #endregion

        #region Property
        private bool ShowLanguage()
        {
            return database != null;
        }
        private List<SystemLanguage> languages
        {
            get
            {
                if (database != null)
                {
                    return database.languages;
                }
                else
                {
                    return new List<SystemLanguage>();
                }
            }
        }

        [ShowIf("ShowLanguage")]
        [ShowInInspector]
        [ValueDropdown("languages")]
        private SystemLanguage language
        {
            get
            {
                return _language;
            }

            set
            {
                if (_language != value)
                {
                    _language = value;
                    ChangeLanguage();
                }
            }
        }
        #endregion

        #region Private
        [SerializeField, HideInInspector]
        private SystemLanguage _language;
        #endregion

        private void OnEnable()
        {
            ChangeLanguage();
        }
        private void Reset()
        {
            #if UNITY_EDITOR
            if (database.IsNull())
                database = Evesoft.Editor.AssetDatabaseFinder.Find<LocalizeDatabase>();
            #endif

            if (!database.IsNull())
                _language = database.language;
        }
        public void ChangeLanguage()
        {
            if (!database.IsNull())
                database.language = _language;
        }
    }
}
#endif
#endif
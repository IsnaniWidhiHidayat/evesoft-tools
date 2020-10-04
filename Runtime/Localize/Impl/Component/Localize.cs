#if ODIN_INSPECTOR 
#if LOCALIZE
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector;

namespace Evesoft.Localize
{
    [Serializable]
    internal class UnityEventString : UnityEvent<string>
    {

    }

    [HideMonoScript]
    [AddComponentMenu(Menu.localize + "/Localize")]
    public class Localize : SerializedMonoBehaviour
    {
        #region Field
        [SerializeField,HideLabel,HorizontalGroup]
        private LocalizeDatabase database;

        private bool ShowLocalizeData => database != null;
        private List<LocalizeData> localizesData()
        {
            if (database != null)
            {
                return database.localize;
            }
            else
            {
                return new List<LocalizeData>();
            }
        }

        [ShowIf("ShowLocalizeData")]
        [ShowInInspector,HideLabel]
        [ValueDropdown("localizesData"),HorizontalGroup(width : 0.7f)]
        private LocalizeData localizeData
        {
            get
            {
                return _localizeData;
            }

            set
            {
                if (_localizeData != value)
                {
                    _localizeData = value;
                    Refresh();
                }
            }
        }

        private bool ShowParameter()
        {
            return localizeData != null && localizeData.parameters.Count > 0;
        }

        [ShowInInspector, ShowIf("ShowParameter")]
        [TableList(AlwaysExpanded = true, IsReadOnly = true, HideToolbar = true)]
        private List<LocalizeParameter> parameters
        {
            get
            {
                if (localizeData != null)
                {
                    text = localizeData.value;
                    return localizeData.parameters;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (localizeData != null)
                {
                    localizeData.parameters = value;
                }
            }
        }

        private bool ShowText()
        {
            return localizeData != null;
        }

        [ShowIf("ShowText")]
        [BoxGroup("Text", false)]
        [ShowInInspector]
        [HideLabel]
        [PropertyOrder(1)]
        [DisplayAsString(overflow: false)]
        private string text;

        private bool ShowOnChange()
        {
            return localizeData != null;
        }

        [PropertyOrder(1)]
        [ShowIf("ShowOnChange")]
        [SerializeField]
        private UnityEventString OnChange;
        #endregion

        #region Private
        [HideInInspector]
        [SerializeField]
        private LocalizeData _localizeData;
        #endregion

        private void OnEnable()
        {
            if (database != null)
                database.onLanguageChange += OnLanguageChange;

            //Add Parameter value change callback
            if (localizeData != null && localizeData.parameters.Count > 0)
            {
                for (int i = 0; i < localizeData.parameters.Count; i++)
                {
                    localizeData.parameters[i].value.OnValueChange += OnParameterValueChange;
                }
            }
        }
        private void OnDisable()
        {
            if (database != null)
                database.onLanguageChange -= OnLanguageChange;

            //Remove parameter value callback
            if (localizeData != null && localizeData.parameters.Count > 0)
            {
                for (int i = 0; i < localizeData.parameters.Count; i++)
                {
                    localizeData.parameters[i].value.OnValueChange -= OnParameterValueChange;
                }
            }
        }
        private void Start()
        {
            Refresh();
        }
        private void Reset()
        {
#if UNITY_EDITOR
            if (database == null)
            {
                database = Utils.AssetDatabaseFinder.Find<LocalizeDatabase>();
            }
#endif
        }

        public void SetParameterValue(string key, string value)
        {
            if (localizeData != null)
            {
                localizeData.SetParameterValue(key, value);
            }
        }
        public void Refresh()
        {
            //Invoke OnChange
            if (OnChange != null && localizeData != null)
            {
                OnChange.Invoke(localizeData.value);
            }
        }

        #region LocalizeCallback
        private void OnParameterValueChange(string value)
        {
            Refresh();
        }
        public void OnLanguageChange(SystemLanguage language)
        {
            Refresh();
        }
        #endregion
    }
}
#endif
#endif
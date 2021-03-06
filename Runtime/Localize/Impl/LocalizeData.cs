#if ODIN_INSPECTOR 
#if LOCALIZE
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Localize
{
    [HideMonoScript]
    [CreateAssetMenu(menuName = nameof(Evesoft) + "/" + nameof(Evesoft.Localize) + "/" + nameof(LocalizeData))]
    [System.Serializable]
    public class LocalizeData : ScriptableObject
    {
        #region Field
        [Required]
        [SerializeField]
        [LabelWidth(70)]
        [HideLabel]
        private LocalizeDatabase database;

        [DisableInPlayMode]
        [SerializeField,HideLabel,SuffixLabel("Key",true)]
        internal string key => name;

        [MultiLineProperty(2)]
        [SerializeField,HideLabel,SuffixLabel("Description",true)]
        private string description;

        [TableList(AlwaysExpanded = true)]
        [DisplayAsString]
        [SerializeField]
        internal List<LocalizeParameter> parameters = new List<LocalizeParameter>();

        //[TableList(AlwaysExpanded = true, IsReadOnly = true, ShowPaging = false, ShowIndexLabels = false, HideToolbar = true)]
        [DisableInPlayMode]
        [SerializeField]
        [ListDrawerSettings(DraggableItems = false,Expanded = true,HideAddButton  =true,HideRemoveButton = true)]
        internal List<LocalizeString> localizes = new List<LocalizeString>();
        #endregion

        #region Property
        public string value
        {
            get
            {
                if (database == null)
                    return string.Empty;

                var language = database.language;
                for (int i = 0; i < localizes.Count; i++)
                {
                    if (localizes[i].language == language)
                    {
                        if (parameters.Count > 0)
                        {
                            string result = localizes[i].text;

                            //Replace Key string to value
                            for (int index = 0; index < parameters.Count; index++)
                            {
                                string key = string.Format("[{0}]", parameters[index].key);
                                if (result.Contains(key))
                                {
                                    result = result.Replace(key, parameters[index].value.value);
                                }
                            }

                            return result;
                        }
                        else
                        {
                            return localizes[i].text;
                        }
                    }
                }

                return string.Empty;
            }
        }
        #endregion

        private void OnEnable()
        {
            #if UNITY_EDITOR
            if (database == null)
            {
                database = Utils.AssetDatabaseFinder.Find<LocalizeDatabase>();
            }

            if (database != null)
            {
                Refresh(database.languages);
            }
            #endif      
        }
        public void Refresh(List<SystemLanguage> languages)
        {
            if (languages == null || languages.Count == 0)
                return;

            //Delete not used Language
            var index = 0;
            while (index < localizes.Count)
            {
                bool contain = languages.Contains(localizes[index].language);
                if (!contain)
                {
                    localizes.Remove(localizes[index]);
                }
                else
                {
                    index++;
                }
            }

            //Add new Localize
            for (int i = 0; i < languages.Count; i++)
            {
                LocalizeString localize = localizes.Find(x => x.language == languages[i]);
                if (localize == null)
                {
                    localizes.Add(new LocalizeString(languages[i], string.Empty));
                }
            }

            //Add to localize Database
            if (database != null)
            {
                database.AddData(this);
            }

            //Sort
            localizes.Sort((x, y) => x.language.CompareTo(y.language));
        }
        public void SetParameterValue(string key, string value)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i] != null && parameters[i].key == key)
                {
                    //Set Value
                    parameters[i].value.value = value;
                    return;
                }
            }
        }

        public override string ToString()
        {
            return key;
        }
    }
} 
#endif
#endif
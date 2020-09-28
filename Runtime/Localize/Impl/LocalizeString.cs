#if ODIN_INSPECTOR 
#if LOCALIZE
using UnityEngine;
using Sirenix.OdinInspector;

namespace Evesoft.Localize
{
    [System.Serializable]
    public class LocalizeString
    {
        #region Field
        [HideInInspector]
        public SystemLanguage language;

        [MultiLineProperty(3),HideLabel,SuffixLabel("$language",true)]
        public string text;
        #endregion

        #region Constructor
        public LocalizeString(SystemLanguage language, string text)
        {
            this.language = language;
            this.text = text;
        }
        #endregion
    }
} 
#endif
#endif
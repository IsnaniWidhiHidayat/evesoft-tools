using Sirenix.OdinInspector;
using System;

namespace Evesoft.Utils
{
    [Serializable]
    public class SemanticVersion:IComparable<SemanticVersion>
    {
        #region Field
        [HorizontalGroup,HideLabel]
        public int major, minor, patch;
        #endregion

        public SemanticVersion(string version)
        {
            string[] str = version.SplitBy('.');
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0)
                {
                    major = str[0].ToInt32();
                }
                else if (i == 1)
                {
                    minor = str[1].ToInt32();
                }
                else if (i == 2)
                {
                    patch = str[2].ToInt32();
                }
            }
        }
        public int CompareTo(SemanticVersion other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                string vx = string.Format("{0}.{1}.{2}", major, minor, patch);
                string vy = string.Format("{0}.{1}.{2}", other.major, other.minor, other.patch);
                return vx.CompareTo(vy);
            }
        }      
        public string ToStringWithoutPatch()
        {
            return string.Format("{0}.{1}", major, minor);
        }
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", major, minor, patch);
        }
    }
}
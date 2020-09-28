#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System;
using UnityEditor;

namespace Evesoft.Editor
{
    public static class AssetDatabaseFinder
    {
        public static T Find<T>() where T : UnityEngine.Object
        {
            string[] find = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T).ToString()), null);
            if (find != null && find.Length > 0)
            {
                return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(find[0]));
            }

            return null;
        }
        public static List<T> Finds<T>() where T : UnityEngine.Object
        {
            string[] split = typeof(T).ToString().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string[] find = AssetDatabase.FindAssets(string.Format("t:{0}", split[split.Length - 1]), null);
            List<T> result = new List<T>();

            for (int i = 0; i < find.Length; i++)
            {
                T add = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(find[i]));
                if (add != null)
                {
                    result.Add(add);
                }
            }

            return result;
        }
        public static List<string> Finds(string type)
        {
            string[] split = type.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string[] find = AssetDatabase.FindAssets(string.Format("t:{0}", split[split.Length - 1]), null);
            List<string> result = new List<string>();

            for (int i = 0; i < find.Length; i++)
            {
                string add = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GUIDToAssetPath(find[i])).name;
                if (add != null)
                {
                    result.Add(add);
                }
            }

            return result;
        }
    }
}  
#endif
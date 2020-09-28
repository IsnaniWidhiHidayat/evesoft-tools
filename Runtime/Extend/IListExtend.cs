#if ODIN_INSPECTOR 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Evesoft
{
    
    public static class IListExtend
    {
        public class ComparisonComparer<T> : IComparer<T>, IComparer
        {
            private readonly Comparison<T> _comparison;

            public ComparisonComparer(Comparison<T> comparison)
            {
                _comparison = comparison;
            }

            public int Compare(T x, T y)
            {
                return _comparison(x, y);
            }

            public int Compare(object o1, object o2)
            {
                return _comparison((T)o1, (T)o2);
            }
        }

        public static T Last<T>(this List<T> target) where T : class
        {
            if (target == null || target.Count == 0)
            {
                return null;
            }
            else
            {
                return target[target.Count - 1];
            }
        }
        public static T First<T>(this List<T> target) where T : class
        {
            if (target == null || target.Count == 0)
            {
                return null;
            }
            else
            {
                return target[0];
            }
        }
        public static string Join<T>(this IList<T> obj, string separator = ",")
        {
            if (obj.IsNullOrEmpty())
                return string.Empty;

            string result = string.Empty;

            for (int i = 0; i < obj.Count; i++)
            {
                result += string.Format("{0}{1}", obj[i], i < obj.Count - 1 ? separator : "");
            }

            return result;
        }
        // public static string Join(this object[] obj, string separator = ",")
        // {
        //     if (obj.IsNullOrEmpty())
        //         return string.Empty;

        //     string result = string.Empty;

        //     for (int i = 0; i < obj.Length; i++)
        //     {
        //         result += string.Format("{0}{1}", obj[i], i < obj.Length - 1 ? separator : "");
        //     }

        //     return result;
        // }
        public static void Shuffle<T>(this IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int k = UnityEngine.Random.Range(0, list.Count);
                T temp = list[k];
                list[k] = list[i];
                list[i] = temp;
            }
        } 
        public static T First<T>(this IList<T> list)
        {
            if(list.IsNullOrEmpty())
                return default(T);

            return list[0];
        }
        public static T Last<T>(this IList<T> list){
            if(list.IsNullOrEmpty())
                return default(T);

            return list[list.Count - 1];
        }
        public static T Find<T>(this IList<T> source, Func<T, bool> condition)
        {
            if(source.IsNullOrEmpty())
                return default(T);

            foreach(T item in source)
                if(condition(item))
                    return item;
                    
            return default(T);
        }
        public static IList<T> FindAll<T>(this IList<T> source, Func<T, bool> condition)
        {
            var result = default(IList<T>);

            foreach(T item in source)
            {
                if(condition(item))
                {
                    if(result.IsNull())
                        result = new List<T>();

                    result.Add(item);
                }
            }
                          
            return result;
        } 
        public static void RemoveAll<T>(this IList<T> source, Func<T, bool> condition)
        {
            if(source.IsNullOrEmpty())
                return;

            var removed = default(IList<T>);

            foreach(var item in source)
            {
                if(condition(item))
                {
                    if(removed.IsNull())
                        removed = new List<T>();

                    removed.Add(item);
                }
            }

            if(!removed.IsNullOrEmpty())
                foreach (var item in removed)
                    source.Remove(item);
        } 
        public static void Sort<T>(this IList<T> list, Comparison<T> comparison)
        {
            ArrayList.Adapter((IList)list).Sort(new ComparisonComparer<T>(comparison));
        }
        public static void Sort<T>(this IList<T> list) where T: IComparable<T>
        {
            Comparison<T> comparison = (l, r) => l.CompareTo(r);
            Sort(list, comparison);

        }
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, Comparison<T> comparison)
        {
            return list.OrderBy(t => t, new ComparisonComparer<T>(comparison));
        }
    }  
}

#endif
#if ODIN_INSPECTOR 
using System.Collections.Generic;
using System.Linq;

namespace Evesoft
{
    public static class NullCheckExtend
    {
        public static bool IsNullOrEmpty<T>(this IList<T> list)
        {
            return list.IsNull() || list.Count == 0;
        }      
        public static bool IsNullOrEmpty<T1,T2>(this IDictionary<T1,T2> dic)
        {
            return dic.IsNull() || dic.Count <= 0;
        }    
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.IsNull() || list.Count() <= 0;
        }    
        public static bool IsNull(this UnityEngine.Object obj)
        {
            return obj == null || obj.Equals(null);
        }
        public static bool IsNull(this object obj)
        {
            return obj == null || obj.Equals(null);
        } 
    }
}

#endif
using System.Collections.Generic;
using UnityEngine;

namespace EveSoft.Utils
{
    public static class LayerMaskUtils
    {
        public static IList<string> GetAllNames()
        {
            var names = new List<string>();

            for (int i = 8; i <= 31; i++)
            {
               var name =  LayerMask.LayerToName(i);
               if(name.IsNullOrEmpty())
                    continue;
            
                names.Add(name);
            }

            return names;
        }
    }
}
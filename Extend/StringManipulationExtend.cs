using System;
using UnityEngine;

namespace EveSoft
{
    public static class StringManipulationExtend
    {
        public static string DeleteWhiteSpace(this string text)
        {
            text = text.Replace(" ", "");
            return text;
        }
        public static string ToColorHtmlFormat(this string text, Color color)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            return string.Format("<color=#{0}>{1}</color>", ColorUtility.ToHtmlStringRGB(color), text);
        }
        public static string[] SplitBy(this string str, char chr = ',')
        {
            return str.Split(new char[] { chr }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

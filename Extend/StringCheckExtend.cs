using System;
using System.Text.RegularExpressions;

namespace EveSoft
{
    public static class StringCheckExtend
    {
        public static bool ContainSymbol(this string text)
        {
            var regexItem = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsValidEmail(this string text)
        {
            string validEmailPattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\\w]*[0-9a-zA-Z])*\\.)+[a-zA-Z]{2,9})$";
            System.Text.RegularExpressions.Regex ValidEmailRegex = new System.Text.RegularExpressions.Regex(validEmailPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return ValidEmailRegex.IsMatch(text);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static int CompareTo(this string strA, string strB, bool ordinal = false, bool ignoreCase = true)
        {
            if (ordinal)
            {
                // check for null values first: a null reference is considered to be less than any reference that is not null
                if (strA == null && strB == null)
                {
                    return 0;
                }
                if (strA == null)
                {
                    return -1;
                }
                if (strB == null)
                {
                    return 1;
                }

                StringComparison comparisonMode = ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture;

                string[] splitX = Regex.Split(strA.Replace(" ", ""), "([0-9]+)");
                string[] splitY = Regex.Split(strB.Replace(" ", ""), "([0-9]+)");

                int comparer = 0;

                for (int i = 0; comparer == 0 && i < splitX.Length; i++)
                {
                    if (splitY.Length <= i)
                    {
                        comparer = 1; // x > y
                    }

                    int numericX = -1;
                    int numericY = -1;
                    if (int.TryParse(splitX[i], out numericX))
                    {
                        if (int.TryParse(splitY[i], out numericY))
                        {
                            comparer = numericX - numericY;
                        }
                        else
                        {
                            comparer = 1; // x > y
                        }
                    }
                    else
                    {
                        comparer = String.Compare(splitX[i], splitY[i], comparisonMode);
                    }
                }

                return comparer;
            }
            else
            {
                return strA.CompareTo(strB);
            }
        }

    }
}

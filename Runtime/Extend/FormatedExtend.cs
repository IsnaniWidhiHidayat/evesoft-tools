#if ODIN_INSPECTOR 


using System;

namespace Evesoft
{
    public static class FormatedExtend
    {
        public static string FormatedValueToTime(this int seconds, string hourFormat = "h", string minuteFormat = "m", string secondsFormat = "s")
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            if (time.Hours > 0)
            {
                return string.Format("{0}{1}", time.Hours, hourFormat);
            }
            else if (time.Minutes > 0)
            {
                return string.Format("{0}{1}", time.Minutes, minuteFormat);
            }
            else
            {
                return string.Format("{0}{1}", time.Seconds, secondsFormat);
            }

        }
        public static string FormatedValueToString(this double count, string HundredLabel = "", string ThousandLabel = "K", string MillionLabel = "M", string BillionLabel = "B")
        {
            string result = string.Empty;

            if (count > 1000000000)//Billion
            {
                var temp = count / 1000000000.0f;

                if (BillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + BillionLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 1000000)//Million
            {
                var temp = count / 1000000.0f;

                if (MillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + MillionLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else if (count > 1000)//ThousandLabel
            {
                var temp = count / 1000.0f;

                if (ThousandLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + ThousandLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 100)
            {
                var temp = count / 100.0f;

                if (HundredLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + HundredLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else
            {
                result = count.ToString();
            }

            return result;
        }
        public static string FormatedValueToString(this int val, string HundredLabel = "", string ThousandLabel = "K", string MillionLabel = "M", string BillionLabel = "B")
        {
            double count = val;
            string result = string.Empty;

            if (count > 1000000000)//Billion
            {
                var temp = count / 1000000000.0f;

                if (BillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + BillionLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 1000000)//Million
            {
                var temp = count / 1000000.0f;

                if (MillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + MillionLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else if (count > 1000)//ThousandLabel
            {
                var temp = count / 1000.0f;

                if (ThousandLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + ThousandLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 100)
            {
                var temp = count / 100.0f;

                if (HundredLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + HundredLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else
            {
                result = count.ToString();
            }

            return result;
        }
        public static string FormatedValueToString(this float val, string HundredLabel = "", string ThousandLabel = "K", string MillionLabel = "M", string BillionLabel = "B")
        {
            double count = val;
            string result = string.Empty;

            if (count > 1000000000)//Billion
            {
                var temp = count / 1000000000.0f;

                if (BillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + BillionLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 1000000)//Million
            {
                var temp = count / 1000000.0f;

                if (MillionLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + MillionLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else if (count > 1000)//ThousandLabel
            {
                var temp = count / 1000.0f;

                if (ThousandLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + ThousandLabel);
                }
                else
                {
                    result = count.ToString();
                }

            }
            else if (count > 100)
            {
                var temp = count / 100.0f;

                if (HundredLabel != string.Empty)
                {
                    result = temp.ToString("#.##" + HundredLabel);
                }
                else
                {
                    result = count.ToString();
                }
            }
            else
            {
                result = count.ToString();
            }

            return result;
        }
        public static string FormatedValueToFileSize(this long val)
        {
            if (val < 1000)
            {
                return string.Format("{0} byte", val);
            }
            else if (val < 1000000)
            {
                return string.Format("{0} Kb", val / 1000f);
            }
            else if (val < 1000000000)
            {
                return string.Format("{0} Mb", val / 1000000f);
            }
            else if (val < 1000000000000)
            {
                return string.Format("{0} Gb", val / 1000000000f);
            }
            else
            {
                return string.Format("{0}", val / 1000000000000f);
            }
        }
        public static string FormatedValueToFileSize(this ulong val)
        {
            if (val < 1000)
            {
                return string.Format("{0} byte", val);
            }
            else if (val < 1000000)
            {
                return string.Format("{0} Kb", val / 1000f);
            }
            else if (val < 1000000000)
            {
                return string.Format("{0} Mb", val / 1000000f);
            }
            else if (val < 1000000000000)
            {
                return string.Format("{0} Gb", val / 1000000000f);
            }
            else
            {
                return string.Format("{0}", val / 1000000000000f);
            }
        }
        public static string ToStringSign(this int value)
        {
            if (value == 0)
            {
                return value.ToString();
            }
            else if (value > 0)
            {
                return "+" + value.ToString();
            }
            else
            {
                return "-" + value.ToString();
            }
        }
    }
}

#endif
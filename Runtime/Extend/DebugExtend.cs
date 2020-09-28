using UnityEngine;
using UnityDebug = UnityEngine.Debug;

namespace Evesoft
{
    public static class DebugExtend
    {
        public static bool debugable = true;

        #region const
        private const string InfoFormat = "[Info]";
        private const string ErrorFormat = "[Error]";
        private const string WarningFormat = "[Warning]";
        private const string ExceptionFormat = "[Exception]";
        #endregion

        #region Debug
        public static void Log(this object message)
        {
            if (debugable)
            {
                UnityDebug.Log(string.Format("{0} {1}", InfoFormat, message));
            }
        }
        public static void Log(this object message, Color color)
        {
            if (debugable)
            {
                UnityDebug.Log(string.Format("<color=#{0}>{1} {2}</color>", UnityEngine.ColorUtility.ToHtmlStringRGBA(color), InfoFormat, message));
            }
        }
        public static void LogFormat(this string format, params object[] args)
        {
            if (debugable)
            {
                string message = string.Format(format, args);
                UnityDebug.LogFormat(string.Format("{0} {1}", InfoFormat, message));
            }
        }
        public static void LogError(this object message)
        {
            if (debugable)
            {
                //UnityDebug.LogError(string.Format("<color=red>{0} {1}</color>", ErrorFormat, message));
                UnityDebug.LogError(message);
            }
        }
        public static void LogErrorFormat(this string format, params object[] args)
        {
            if (debugable)
            {
                string message = string.Format(format, args);
                UnityDebug.LogErrorFormat(string.Format("{0} {1}", ErrorFormat, message));
            }
        }
        public static void LogException(this System.Exception exception)
        {
            if (debugable)
            {
                UnityDebug.LogException(exception);
            }
        }
        public static void LogWarning(this object message)
        {
            if (debugable)
            {
                UnityDebug.LogWarning(string.Format("<color=yellow>{0} {1}</color>", WarningFormat, message));
            }
        }
        public static void LogWarningFormat(this string format, params object[] args)
        {
            if (debugable)
            {
                string message = string.Format(format, args);
                UnityDebug.LogErrorFormat(string.Format("<color=yellow>{0} {1}</color>", WarningFormat, message));
            }
        }
        
        public static void LogFilter(this string str,string filter)
        {
            if (debugable)
            {
                var command = "\nCPAPI:{\"cmd\":\"Filter\" \"name\":\"" + filter + "\"}";
                (str + command).Log();
            }
        }
        public static void LogFilterFormat(this string format,string filter, params object[] args)
        {
            if (debugable)
            {
                var command = "\nCPAPI:{\"cmd\":\"Filter\" \"name\":\"" + filter + "\"}";
                var message = string.Format(format, args);
                    message = string.Format("{0} {1}",message,command);
                    message.Log();
            }
        }
        public static void LogWatch(this string str,string name)
        {
            if(debugable) 
            {
                var command = "\nCPAPI:{\"cmd\":\"Watch\" \"name\":\"" + name + "\"}";
                (str + command).Log();
            }
        }
        public static void LogWatchFormat(this string format,string name, params object[] args)
        {
            if (debugable)
            {
                var command = "\nCPAPI:{\"cmd\":\"Watch\" \"name\":\"" + name + "\"}";
                var message = string.Format(format, args);
                    message = string.Format("{0} {1}",message,command);
                    message.Log();
            }
        }
        #endregion

#if UNITY_EDITOR
        public static void ClearConsole(this object obj) 
        {
            // This simply does "LogEntries.Clear()" the long way:
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
#endif
        
    }
}
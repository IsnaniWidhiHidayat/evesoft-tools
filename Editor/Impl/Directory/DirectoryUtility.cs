#if ODIN_INSPECTOR 
using UnityEngine;
using UnityEditor;

namespace Evesoft.Editor.Directory
{
    public static class DirectoryUtility
    {
        public static string projectLocation => Application.dataPath;
        public static string persistentLocation => Application.persistentDataPath;
        public static string cacheLocation => Application.temporaryCachePath;
        public static string consoleLocation => Application.consoleLogPath;
        public static string streamAssetLocation => Application.streamingAssetsPath;

        public static void OpenDir()
        {
            var path = projectLocation;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }
        public static void OpenPersistentDirectory()
        {
            var path = persistentLocation;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }
        public static void OpenCacheDirectory()
        {
            var path = cacheLocation;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }    
        public static void OpenConsoleDir()
        {
            var path = consoleLocation;
            if(path.FileExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","File not exist","ok");
        }
        public static void OpenStreamAssetsDir()
        {
            var path = streamAssetLocation;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }
    } 
}

#endif
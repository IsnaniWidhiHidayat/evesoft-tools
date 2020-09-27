using UnityEngine;
using UnityEditor;

namespace Evesoft.Editor.Directory
{
    internal static class DirectoryProject
    {
        #region const
        const string grpPath = "Tools/EveSoft/Directory/";
        #endregion

        [MenuItem(grpPath + "Data Path")]
        private static void OpenDir()
        {
            var path = Application.dataPath;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }

        [MenuItem(grpPath + "Persitent")]
        private static void OpenPersistentDirectory()
        {
            var path = Application.persistentDataPath;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }

        [MenuItem(grpPath + "Cache")]
        private static void OpenCacheDirectory()
        {
            var path = Application.temporaryCachePath;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }    
    
        [MenuItem(grpPath + "Console")]
        private static void OpenConsoleDir()
        {
            var path = Application.consoleLogPath;
            if(path.FileExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","File not exist","ok");
        }
    
        [MenuItem(grpPath + "Stream Assets")]
        private static void OpenStreamAssetsDir()
        {
            var path = Application.streamingAssetsPath;
            if(path.DirectoryExist())
                System.Diagnostics.Process.Start(path);
            else
                EditorUtility.DisplayDialog("Message","Directory not exist","ok");
        }
    } 
}

#if ODIN_INSPECTOR 
using System;
using System.IO;
using UnityEngine;

namespace Evesoft
{
    public static class IOExtend
    {
        public static void WriteToFile(this byte[] bytes, string path)//,bool direct = false)
        {
            if (bytes.IsNull() || path.IsNullOrEmpty())
                return;

            try
            {
                string dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // if(direct)
                // {
                    File.WriteAllBytes(path, bytes);
                // }
                // else
                // {
                    // var future = new UnityToolbag.Future<bool>();
                    // future.Process(()=>
                    // {       
                        File.WriteAllBytes(path, bytes);
                    //     return true;
                    // });
                // }
            }
            catch (Exception ex)
            {
                ex.Message.Log();
            }
        }
        public static void WriteToFile(this string str, string path)//,bool direct = false)
        {
            if (path.IsNullOrEmpty())
                return;

            try
            {
                string dir = Path.GetDirectoryName(path);

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                // if(direct)
                // {
                //     File.WriteAllText(path, str);
                // }
                // else
                // {
                //     var future = new UnityToolbag.Future<bool>();
                //     future.Process(()=>
                //     {       
                        File.WriteAllText(path, str);
                        //return true;
                //     });
                // }
            }
            catch (Exception ex)
            {
                ex.Message.Log();
            }
        }
        public static void WriteToFile(this Texture2D texture, string path)//,bool direct = false)
        {
            if (path.IsNullOrEmpty())
                return;

            string extension = Path.GetExtension(path).ToLowerInvariant();

            switch (extension)
            {
                case ".jpg":
                    {
                        texture.EncodeToJPG().WriteToFile(path);//,direct);
                        break;
                    }

                case ".jpeg":
                    {
                        texture.EncodeToJPG().WriteToFile(path);//,direct);
                        break;
                    }

                case ".png":
                    {
                        texture.EncodeToPNG().WriteToFile(path);//,direct);
                        break;
                    }

                case ".tga":
                    {
                        texture.EncodeToTGA().WriteToFile(path);//,direct);
                        break;
                    }

                case ".exr":
                    {
                        texture.EncodeToEXR().WriteToFile(path);//,direct);
                        break;
                    }

                default:
                    {
                        texture.EncodeToPNG().WriteToFile(path);//,direct);
                        break;
                    }
            }
        }
        public static void OpenFileWithPath(this string path)
        {
            if (!File.Exists(path))
                return;

            try
            {
                System.Diagnostics.Process.Start(Path.GetFullPath(path));
            }
            catch (Exception ex)
            {
                ex.Message.LogError();
            }
        }
        public static void OpenFolderWithPath(this string path)
        {
            try
            {
                string directory = Path.GetDirectoryName(path);

                if (Directory.Exists(directory))
                {
                    System.Diagnostics.Process.Start(directory);
                }
            }
            catch (Exception ex)
            {
                ex.Message.LogError();
            }
        }
        public static string ReadFile(this string path)
        {
            if (path.IsNullOrEmpty() || !File.Exists(path))
                return null;

            return File.ReadAllText(path);
        }
        public static byte[] ReadFileBytes(this string path)
        {
            if (path.IsNullOrEmpty() || !File.Exists(path))
                return null;

            return File.ReadAllBytes(path);
        }
        public static bool FileExist(this string path)
        {
            if (path.IsNullOrEmpty())
                return false;

            return File.Exists(path);
        }
        public static bool DirectoryExist(this string path)
        {
            if (path.IsNullOrEmpty())
                return false;

            return Directory.Exists(path);
        }
        public static void RemoveFile(this string path)
        {
            if (path.IsNullOrEmpty() || !File.Exists(path))
                return;

            File.Delete(path);
        }
        public static string CombinePath(this string path, string pathB)
        {
            var result = Path.Combine(path, pathB);
            return result;
        }
        
        public static void CopyFileTo(this string path, string destination)
        {
            if (path.IsNullOrEmpty() || destination.IsNullOrEmpty())
                return;

            File.Copy(path, destination, true);
        }
        public static string GetDiretoryName(this string path){
            if(path.IsNullOrEmpty())
                return null;

            return Path.GetDirectoryName(path);
        }
        public static string GetFileName(this string path){
            if(path.IsNullOrEmpty())
                return null;

            return Path.GetFileName(path);
        }
        public static string GetFileNameWithoutExtension(this string path){
            if(path.IsNullOrEmpty())
                return null;

            return Path.GetFileNameWithoutExtension(path);
        }
        public static void CreateDirectoryIfNotExist(this string path)
        {
            if (path.IsNullOrEmpty())
                return;

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public static void RemoveDirectory(this string path)
        {
            if (path.IsNullOrEmpty() || !Directory.Exists(path))
                return;

            Directory.Delete(path);
        }
        public static string ReplaceDirectorySeparator(this string str, char replace = '/')
        {
            if (str.Contains("\\"))
            {
                return str.Replace('\\', replace);
            }
            else
            {
                return str;
            }
        }
        public static void ClearDirectory(this string path)
        {
            if(path.IsNullOrEmpty())
                return;

            var di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles())
                file.Delete(); 

            
            foreach (var dir in di.GetDirectories())
                dir.Delete(true); 
        }  
        public static string[] GetFiles(this string path,string extension)
        {
            return Directory.GetFiles(path,extension,SearchOption.AllDirectories);
        }
    }
}

#endif
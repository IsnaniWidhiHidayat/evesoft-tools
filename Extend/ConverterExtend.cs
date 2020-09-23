using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;

namespace Evesoft
{
    public static class ConverterExtend
    {
        private const string dateTimeFormat = dateFormat + " HH:mm:ss:ff";
        private const string dateFormat = "dd-MM-yyyy";
        private static JsonSerializerSettings JsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include, TypeNameHandling = TypeNameHandling.All, DefaultValueHandling = DefaultValueHandling.Include };

        public static bool ToBoolean(this object obj)
        {
            try
            {
                return System.Convert.ToBoolean(obj);
            }
            catch (System.Exception ex)
            {
                ex.Message.Log();
            }

            return false;
        }
        public static int ToInt32(this object obj)
        {
            try
            {
                return System.Convert.ToInt32(obj);
            }
            catch (System.Exception ex)
            {
                ex.Message.Log();
            }

            return 0;
        }
        public static long ToInt64(this object obj)
        {
            return System.Convert.ToInt64(obj);
        }
        public static ulong ToUInt64(this object obj)
        {
            return System.Convert.ToUInt64(obj);
        }
        public static float ToSingle(this object obj)
        {
            try
            {
                return System.Convert.ToSingle(obj);
            }
            catch (System.Exception ex)
            {
                ex.Message.Log();
            }

            return 0.0f;
        }

        public static Texture2D ToTexture2D(this byte[] data,bool mipchain = false)
        {
            if (data == null)
                return null;

            Texture2D result = new Texture2D(1, 1, TextureFormat.ARGB32,mipchain);
            if(result.LoadImage(data))
                result.Apply();

            return result;
        }
        public static Texture2D ToTexture2D(this Sprite sprite,bool mipchain = false)
        {
            var texture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.ARGB32, mipchain);
            var pixels = sprite.texture.GetPixels((int)sprite.rect.x, (int)sprite.rect.y, (int)sprite.rect.width, (int)sprite.rect.height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }
        public static Texture2D ToTexture2D(this RenderTexture target, ref Texture2D tex,bool mipchain = false)
        {
            if (tex == null)
                tex = new Texture2D(target.width, target.height, TextureFormat.RGB24, mipchain);

            RenderTexture.active = target;
            tex.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
            tex.Apply();
            return tex;
        }
        public static Sprite ToSprite(this Texture2D texture, Rect rect, Vector2 pivot, float pixelPerUnit = 100)
        {
            if (texture == null)
            {
                return null;
            }
            else
            {                
                return Sprite.Create(texture, rect, pivot, pixelPerUnit,0,SpriteMeshType.FullRect);
            }
        }
        public static Sprite ToSprite(this Texture2D texture, Rect rect, float pixelPerUnit = 100)
        {
            if (texture == null)
            {
                return null;
            }
            else
            {
                return Sprite.Create(texture, rect, Vector2.one * 0.5f, pixelPerUnit,0,SpriteMeshType.FullRect);
            }
        }
        public static Sprite ToSprite(this Texture2D texture, float pixelPerUnit = 100)
        {
            if (texture.IsNull())
                return null;

            return ToSprite(texture, new Rect(0, 0, texture.width, texture.height), pixelPerUnit);
        }       
        public static DateTime ToDateTime(this string timeString, string format = dateTimeFormat)
        {
            return DateTime.ParseExact(timeString, format, null);
        }
        public static DateTime ToDate(this string timeString, string format = dateFormat)
        {
            return DateTime.ParseExact(timeString, format, null);
        }  
        public static string GetString(this byte[] bytes)
        {
            if(bytes.IsNullOrEmpty())
                return string.Empty;

            return Encoding.ASCII.GetString(bytes);
        }
        public static byte[] ToBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        public static string ToStringDateTime(this DateTime dateTime, string format = dateTimeFormat)
        {
            return dateTime.ToString(format);
        }
        public static string ToStringDate(this DateTime dateTime, string format = dateFormat)
        {
            return dateTime.ToString(format);
        }
        public static bool To<T>(this object obj, out T result)
        {
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            result = default(T);
            return false;
        }
    }
}

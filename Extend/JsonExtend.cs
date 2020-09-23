using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Evesoft
{
    public static class JsonExtend
    {
        public static bool IsValidJson(this string strInput)
        {
            if(strInput.IsNullOrEmpty())
                return false;

            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    jex.Message.LogError();
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    ex.Message.LogError();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }   
        public static (string,Exception) ToJson(this object obj)
        {      
            try
            {
                if(obj.IsNull())
                    return (default(string),new System.ArgumentNullException("obj"));

                string json = JsonConvert.SerializeObject(obj,Formatting.Indented);
                return (json,null);
            }
            catch (Exception ex)
            {
                ex.Message.LogError();
                return (null,ex);
            }
        }
        public static (string,Exception) ToJsonUnity(this object obj)
        {
            try
            {
                if(obj.IsNull())
                    return (default(string),new System.ArgumentNullException("obj"));

                string json = JsonUtility.ToJson(obj);
                return (json,null);
            }
            catch (Exception ex)
            {
                ex.Message.LogError();
                return (null,ex);
            }
        }
        public static (T,Exception) FromJson<T>(this string json)
        {
            try
            {
                if(!json.IsValidJson())
                    return (default(T),new System.ArgumentException("json","Not valid json format"));

                var setting = new JsonSerializerSettings(){ NullValueHandling = NullValueHandling.Ignore };
                var result  = JsonConvert.DeserializeObject<T>(json,setting);
                return (result,null);
            }
            catch (System.Exception ex)
            {
                ex.Message.LogError();
                return (default(T),ex);
            }
        }
        public static (T,Exception) FromJsonUnity<T>(this string json)
        {
            try
            {
                if(!json.IsValidJson())
                    return (default(T),new System.ArgumentException("json","Not valid json format"));

                var result  = JsonUtility.FromJson<T>(json);
                return (result,null);
            }
            catch (System.Exception ex)
            {
                return (default(T),ex);
            }
        }
    }
}
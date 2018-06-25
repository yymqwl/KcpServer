using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace GameFramework
{
    public static class  JsonUtils
    {
        public static Byte[] JsonSerialize(object obj)
        {
            
           return  Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));

        }
        public static T JsonDeserialize<T>(Byte[] data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
            }
            catch (Exception ex)
            {
                throw new GameFrameworkException("JsonDeserialize type: " + ex.ToString());
                //AppModuleManager.Instance.LogModule.Log.Error("JsonDeserialize type: " +ex.ToString());
            }
            //return default(T);
            //return (T)Deserialize(data, typeof(T));

        }
        public static string JsonSerialize_String(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T JsonDeserialize_String<T>(string data)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception ex)
            {
                throw new GameFrameworkException("JsonDeserialize type: " + ex.ToString());
              
            }
        }
    }
}

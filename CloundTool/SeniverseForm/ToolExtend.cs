using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeniverseForm
{
    public static class ToolExtend
    {
        /// <summary>
        /// HMACSHA1 加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <param name="Key">秘钥</param>
        /// <returns></returns>
        public static string ToBase64hmac(this string source, string Key)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(Key))
            {
                return string.Empty;
            }
            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(Key));
            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(source));
            return System.Convert.ToBase64String(byteText);
        }


        /// <summary>
        /// 根据枚举字段的Display特性的说明
        /// </summary>
        /// <param name="e">枚举字段</param>
        /// <returns></returns>
        public static string GetFieldDescription(this Enum e)
        {
            if (e == null)
            {
                return null;
            }
            var field = e.GetType().GetField(e.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;
            return attribute == null ? null : attribute.Description;
        }

        /// <summary>  
        /// 获取时间戳  13位
        /// </summary> 
        /// <returns></returns>  
        public static TimeSpan GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts;
        }

        /// <summary>
        /// 获取新的Path与Query
        /// </summary>
        /// <param name="pathQuery">原始path与query</param>
        /// <param name="keyValue">键值对</param>
        /// <returns></returns>
        public static Uri UriContains(this Uri url, IEnumerable<KeyValuePair<string, string>> keys)
        {
            foreach (var item in keys)
            {
                url = url.UriContains(item.Key, item.Value);
            }
            return url;
        }

        /// <summary>
        /// 获取新的Path与Query
        /// </summary>
        /// <param name="pathQuery">原始path与query</param>
        /// <param name="keyValue">键值对</param>
        /// <returns></returns>
        public static Uri UriContains(this Uri url, string key, string value)
        {
            var query = string.Format("{0}={1}", key, value);
            var concat = url.ToString().Contains('?') ? "&" : "?";
            return new Uri(url + concat + query);
        }
    }
}

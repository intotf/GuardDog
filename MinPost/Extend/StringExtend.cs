using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MinPost
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static partial class StringExtend
    {
        /// <summary>
        /// 字符串转 Uri
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Uri ToUri(this string source)
        {
            return new Uri(source);
        }

        /// <summary>
        /// 判断一个字符串是否为url
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsUrl(this string str)
        {
            try
            {
                string Url = @"(http|https):\/\/([\w.]+\/?)\S*";
                return Regex.IsMatch(str, Url);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinPost
{
    /// <summary>
    /// Object 转任意格式
    /// </summary>
    public static partial class ObjectExtend
    {
        /// <summary>
        /// Object 强制转换任意格式数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T ToOfType<T>(this object source)
        {
            return (T)source;
        }
    }
}

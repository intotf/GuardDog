using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace WeatherLib
{
    /// <summary>
    ///  Rest 实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiRestModel<T> where T : class
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Api Rest风格
    /// </summary>
    public class ApiRestReulst
    {
        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiRestModel<T> True<T>(T data) where T : class
        {
            return True(data, StatusCode.None.GetHashCode(), StatusCode.None.GetFieldDescription());
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiRestModel<T> True<T>(T data, StatusCode code) where T : class
        {
            return True(data, StatusCode.None.GetHashCode(), StatusCode.None.GetFieldDescription());
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiRestModel<T> True<T>(T data, int code, string msg) where T : class
        {
            return new ApiRestModel<T> { Code = code, Data = data, Msg = msg, State = true };
        }

        /// <summary>
        /// 异常返回,数据格式 object
        /// </summary>
        /// <param name="code">错误枚举</param>
        /// <returns></returns>
        public static ApiRestModel<object> False(StatusCode code)
        {
            return False<object>(code);
        }

        /// <summary>
        /// 根据错误枚举
        /// 返回异常
        /// </summary>
        /// <param name="code">错误枚举</param>
        /// <returns></returns>
        public static ApiRestModel<T> False<T>(StatusCode code) where T : class
        {
            return False(code.GetHashCode(), code.GetFieldDescription(), default(T));
        }

        /// <summary>
        /// 异常返回,数据格式 object
        /// </summary>
        /// <param name="code">错误编码</param>
        /// <param name="msg">提示消息</param>
        /// <returns></returns>
        public static ApiRestModel<object> False(int code, string msg)
        {
            return False(code, msg, default(object));
        }

        /// <summary>
        /// 异常返回
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="msg">提示消息</param>
        /// <param name="data">数据内容</param>
        /// <returns></returns>
        public static ApiRestModel<T> False<T>(int code, string msg, T data) where T : class
        {
            return new ApiRestModel<T> { Code = code, Data = data, Msg = msg, State = false };
        }
    }
}

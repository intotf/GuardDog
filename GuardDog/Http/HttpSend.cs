using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure;

namespace GuardDog
{
    /// <summary>
    /// Http 通知
    /// </summary>
    public class HttpSend
    {
        /// <summary>
        /// http 通知配置信息
        /// </summary>
        private static readonly HttpNotify httpConfig = DogConfig.Instance.HttpNotify;

        /// <summary>
        /// 记录已发送的信息
        /// </summary>
        private static DateTime LastSendTime = DateTime.Now;

        /// <summary>
        /// 发送Http 通知
        /// </summary>
        /// <param name="TargetName">服务/进程名称</param>
        /// <param name="SystemName">服务/进程</param>
        /// <param name="msg">提示消息</param>
        public static async Task Send(string msg, string TargetName, string SystemName)
        {
            if (!httpConfig.URL.ToString().IsNullOrEmpty() && LastSendTime.AddMinutes(httpConfig.MaxInterval) < DateTime.Now)
            {
                using (var httpClient = new WebClient())
                {
                    httpClient.Headers.Add("Auth", httpConfig.Auth);
                    httpClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                    var strData = string.Format("msg={0}&TargetName={1}&SystemName={2}", msg, TargetName, SystemName);
                    var byteData = Encoding.UTF8.GetBytes(strData);
                    httpClient.UploadDataAsync(httpConfig.URL, "POST", byteData);
                    LastSendTime = DateTime.Now;
                }
            }
        }
    }
}

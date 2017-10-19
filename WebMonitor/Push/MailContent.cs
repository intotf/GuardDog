using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace WebMonitor.Push
{
    /// <summary>
    /// 发送邮件内容
    /// </summary>
    public class MailContent
    {
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content
        {
            get;
            private set;
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string WebName { get; set; }

        /// <summary>
        /// 站点Url
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return DateTime.Now;
            }
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="Message"></param>
        /// <param name="SystemName"></param>
        public MailContent(string WebName, string WebUrl, string Message)
        {
            this.WebName = WebName;
            this.WebUrl = WebUrl;
            this.Message = Message;
            this.Content = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "MailModel.html")
                                        .Replace("{WebName}", this.WebName)
                                        .Replace("{WebUrl}", this.WebUrl)
                                        .Replace("{Message}", this.Message)
                                        .Replace("{DateTime}", this.DateTime.ToDateTimeString());
        }
    }
}

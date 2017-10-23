using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace GuardDog
{
    public class MailContent
    {
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string content
        {
            get;
            private set;
        }

        /// <summary>
        /// 目标程序/服务名称
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

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
        public MailContent(string Target, string Message, string SystemName)
        {
            this.Target = Target;
            this.Message = Message;
            this.SystemName = SystemName;
            this.content = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "MailModel.html")
                                        .Replace("{Target}", this.Target)
                                        .Replace("{SystemName}", this.SystemName)
                                        .Replace("{Message}", this.Message)
                                        .Replace("{DateTime}", this.DateTime.ToDateTimeString());
        }
    }
}

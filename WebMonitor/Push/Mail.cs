using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebMonitor.Push
{
    public class Mail
    {
        public static Regex r = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");

        /// <summary>
        /// 配置
        /// </summary>
        public static readonly MailConfig config = MailConfig.Instance;

        /// <summary>
        /// 邮件
        /// </summary>
        private static readonly EMail email = new EMail(config.From, config.Password, config.Smtp, config.Port);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="title">标题</param>
        /// <param name="htmlBody">html内容</param>
        /// <returns></returns>
        public static async Task SendAsync(string to, string title, string htmlBody)
        {
            await email.SendAsync(to, title, htmlBody, config.SSL);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="to">接收者</param>
        /// <param name="title">标题</param>
        /// <param name="htmlBody">html内容</param>
        /// <returns></returns>
        public static async Task SendAsync(IEnumerable<string> to, string title, string htmlBody)
        {
            await email.SendAsync(to, title, htmlBody, config.SSL);
        }
    }

    /// <summary>
    /// 邮件配置
    /// </summary>
    public class MailConfig : ConfigurationSection
    {

        public static readonly MailConfig Instance = ConfigurationManager.GetSection("Mail") as MailConfig;

        /// <summary>
        /// Smtp
        /// </summary>
        [ConfigurationProperty("Smtp", DefaultValue = "mail.163.com")]
        public string Smtp
        {
            get
            {
                return (string)base["Smtp"];
            }
        }

        /// <summary>
        /// 是否SSL
        /// </summary>
        [ConfigurationProperty("Port", DefaultValue = 25)]
        public int Port
        {
            get
            {
                return (int)base["Port"];
            }
        }


        /// <summary>
        /// 是否SSL
        /// </summary>
        [ConfigurationProperty("SSL", DefaultValue = false)]
        public bool SSL
        {
            get
            {
                return (bool)base["SSL"];
            }
        }

        /// <summary>
        /// 发送者
        /// </summary>
        [ConfigurationProperty("From")]
        public string From
        {
            get
            {
                return (string)base["From"];
            }
        }

        /// <summary>
        /// 发送者密码
        /// </summary>
        [ConfigurationProperty("Password")]
        public string Password
        {
            get
            {
                return (string)base["Password"];
            }
        }
    }
}

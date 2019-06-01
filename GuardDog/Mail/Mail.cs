using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardDog
{
    public static class Mail
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static readonly MailConfig config = ConfigurationManager.GetSection("Mail") as MailConfig;

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
}

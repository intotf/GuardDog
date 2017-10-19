using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMonitor.Push
{
    /// <summary>
    /// 消息推送
    /// </summary>
    public class BastPush : IPush
    {
        /// <summary>
        /// 发送消息体
        /// </summary>
        public MailContent Data { get; set; }

        public BastPush(MailContent Data)
        {
            this.Data = Data;
        }

        public async Task SendAsync()
        {
            if (Config.Instance.IsSendMail)
            {
                foreach (var item in Config.Instance.Emails)
                {
                    var title = string.Format("[{0}] - 网站监控服务", Data.WebName);
                    await Mail.SendAsync(item, title, Data.Content);
                }
            }
        }
    }
}

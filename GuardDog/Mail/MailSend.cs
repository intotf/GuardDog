using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardDog
{
    /// <summary>
    /// 发邮件方法
    /// </summary>
    public class MailSend
    {
        /// <summary>
        /// 记录已发送的信息
        /// </summary>
        private static List<SendRecord> SendRecord = new List<SendRecord>();

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="TargetName">服务/进程名称</param>
        /// <param name="SystemName">服务/进程</param>
        /// <param name="msg">提示消息</param>
        public static async Task SendMail(string msg, string TargetName, string SystemName)
        {
            if (DogConfig.Instance.IsSend)
            {
                var MailContent = new MailContent(TargetName, msg, SystemName);
                foreach (var item in DogConfig.GetConfigMails())
                {
                    var hashValue = (MailContent.Target + item).GetHashCode();
                    var RecordModel = SendRecord.Where(it => it.hashValue == hashValue).FirstOrDefault();
                    if (RecordModel == null)
                    {
                        SendRecord.Add(new SendRecord()
                        {
                            TargetName = MailContent.Target,
                            address = item,
                            LastSendTime = DateTime.Now,
                            hashValue = hashValue
                        });
                        await Mail.SendAsync(item, "看门狗异常提醒", MailContent.content);
                    }
                    else if (RecordModel.LastSendTime.AddMinutes(Mail.config.MaxInterval) < DateTime.Now)
                    {
                        RecordModel.LastSendTime = DateTime.Now;
                        await Mail.SendAsync(item, "看门狗异常提醒", MailContent.content);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardDog
{
    /// <summary>
    /// 邮件发送限制
    /// </summary>
    public class SendRecord
    {
        /// <summary>
        /// hash唯一值
        /// </summary>
        public int hashValue { get; set; }

        /// <summary>
        /// 服务/应用程序名称
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// 要发送的地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 同一个异常发送次数
        /// </summary>
        public DateTime LastSendTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardDog
{
    /// <summary>
    /// 邮件配置
    /// </summary>
    public class MailConfig : ConfigurationSection
    {

        public static readonly DogConfig Instance = ConfigurationManager.GetSection("MonitorConfig") as DogConfig;

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

        /// <summary>
        /// 同一个异常发送邮件间隔时长
        /// 0 不限制；单位分钟
        /// </summary>
        [ConfigurationProperty("MaxInterval", DefaultValue = 0)]
        public int MaxInterval
        {
            get
            {
                return (int)base["MaxInterval"];
            }
        }
    }
}

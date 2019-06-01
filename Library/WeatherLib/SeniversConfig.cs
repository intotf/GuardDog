using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib
{
    /// <summary>
    /// 心知天气配置
    /// </summary>
    public class SeniversConfig : ConfigurationSection
    {
        public static readonly SeniversConfig Instance = ConfigurationManager.GetSection("SeniversConfig") as SeniversConfig;

        /// <summary>
        /// 用户ID
        /// </summary>
        [ConfigurationProperty("Uid")]
        public string Uid
        {
            get
            {
                return (string)base["Uid"];
            }
            set
            {
                base["Uid"] = value;
            }
        }

        /// <summary>
        /// 用户Key
        /// </summary>
        [ConfigurationProperty("Key")]
        public string Key
        {
            get
            {
                return (string)base["Key"];
            }
            set
            {
                base["Key"] = value;
            }
        }

        /// <summary>
        /// 有效时长 单位秒
        /// </summary>
        [ConfigurationProperty("Ttl", DefaultValue = 30)]
        public int Ttl
        {
            get
            {
                return (int)base["Ttl"];
            }
            set
            {
                base["Ttl"] = value;
            }
        }
    }
}

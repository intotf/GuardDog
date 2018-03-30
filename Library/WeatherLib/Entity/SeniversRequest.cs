using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherLib
{
    /// <summary>
    /// 心知天气请求体
    /// </summary>
    public class SeniversRequest
    {
        /// <summary>
        /// 配置信息
        /// </summary>
        private readonly SeniversConfig config = SeniversConfig.Instance;

        /// <summary>
        /// 查询地区
        /// </summary>
        public string location { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string ts { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        public int ttl { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sig { get; set; }

        /// <summary>
        /// 构造对象
        /// </summary>
        /// <param name="location">查询城市</param>
        public SeniversRequest(string location)
        {
            this.uid = config.Uid;
            this.ttl = config.Ttl;
            this.location = location;
            this.ts = (DateTime.Now.ToTimeStamp()).ToString();
            var signText = string.Format("ts={0}&ttl={1}&uid={2}", this.ts, this.ttl, this.uid);
            this.sig = signText.ToBase64hmac(config.Key);
        }
    }
}

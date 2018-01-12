using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace SeniverseForm
{
    public interface SeniverseApi : IDisposable
    {
        /// <summary>
        /// 获取逐日天气
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [SeniversFilter]
        [SeniversReturn]
        [HttpHost("https://api.seniverse.com/v3/weather/daily.json")]
        ITask<SeniversResult> GetDailyAsync(string location);

        /// <summary>
        /// 从 ip138 站点获取IP
        /// </summary>
        /// <returns></returns>
        [HttpHost("http://2017.ip138.com/ic.asp")]
        [IpReturn]
        [Timeout(3000)]
        [Header("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36")]
        ITask<string> GetIp();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace WeatherLib
{
    public interface SeniverseApi : IDisposable
    {
        /// <summary>
        /// 获取逐日天气
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SeniversFilter]
        [SeniversReturn]
        [HttpHost("https://api.seniverse.com/v3/weather/daily.json")]
        ITask<SeniversResult> GetDailyAsync(SeniversRequest model);


        /// <summary>
        /// 获取逐日天气
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [SeniversFilter]
        [SeniversRestReturn]
        [HttpHost("https://api.seniverse.com/v3/weather/daily.json")]
        ITask<ApiRestModel<SeniversResult>> GetDailyRestAsync(SeniversRequest model);
    }
}

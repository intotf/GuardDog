using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;

namespace WeatherLib
{
    /// <summary>
    /// 天气接口
    /// </summary>
    public class WeatherApi
    {
        /// <summary>
        /// 加载配置信息
        /// </summary>
        private static readonly SeniversConfig config = SeniversConfig.Instance;

        private static readonly SeniverseApi api = HttpApiClient.Create<SeniverseApi>();

        /// <summary>
        /// 获取三天天气数据
        /// </summary>
        /// <param name="area">地区名</param>
        /// <returns></returns>
        public static async Task<SeniversResult> GetByDaily(string area)
        {
            var data = await api.GetDailyAsync(new SeniversRequest(area)).Retry(3, TimeSpan.FromSeconds(2))
                .WhenResult(txt => txt == null || txt.results == null || txt.results.Length == 0)
                .Handle()
                .WhenCatch<SeniverserException>(ex =>
                {
                    return default(SeniversResult);
                });
            return data;
        }

        /// <summary>
        /// 获取当天天气数据
        /// </summary>
        /// <param name="area">地区名</param>
        /// <returns></returns>
        public static async Task<Daily> GetByToday(string area)
        {
            var data = await api.GetDailyAsync(new SeniversRequest(area)).Retry(3, TimeSpan.FromSeconds(2))
                .WhenResult(txt => txt == null || txt.results == null || txt.results.Length == 0)
                .Handle()
                .WhenCatch<SeniverserException>(ex =>
                {
                    return default(SeniversResult);
                });
            if (data == null)
            {
                return null;
            }
            return data.results.FirstOrDefault().daily.FirstOrDefault();
        }

        /// <summary>
        /// 使用 ApiRestModel 风格获取3天气数据
        /// </summary>
        /// <param name="area">地区名称</param>
        /// <returns></returns>
        public static async Task<ApiRestModel<SeniversResult>> GetDailyRestAsync(string area)
        {
            var data = await api.GetDailyRestAsync(new SeniversRequest(area)).Retry(3, TimeSpan.FromSeconds(2))
                .WhenResult(t => t.State == false)
                .Handle()
                .WhenCatch<SeniverserException>(ex =>
                {
                    return ApiRestReulst.False<SeniversResult>(ex.Code);
                });
            return data;
        }

        /// <summary>
        /// 使用 ApiRestModel 风格获取当天天气数据
        /// </summary>
        /// <param name="area">地区名称</param>
        /// <returns></returns>
        public static async Task<ApiRestModel<Daily>> GetTodayRestAsync(string area)
        {
            var data = await api.GetDailyRestAsync(new SeniversRequest(area)).Retry(3, TimeSpan.FromSeconds(2))
                .WhenResult(t => t.State == false)
                .Handle()
                .WhenCatch<SeniverserException>(ex =>
                {
                    return ApiRestReulst.False<SeniversResult>(ex.Code);
                });

            if (!data.State)
            {
                return ApiRestReulst.False<Daily>((StatusCode)data.Code);
            }

            return ApiRestReulst.True(data.Data.results.FirstOrDefault().daily.FirstOrDefault());
        }
    }
}

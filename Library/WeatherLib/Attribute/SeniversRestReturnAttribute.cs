using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace WeatherLib
{
    /// <summary>
    /// 对返回数据进行处理
    /// </summary>
    public class SeniversRestReturnAttribute : ApiReturnAttribute
    {
        public SeniversRestReturnAttribute()
        {

        }

        /// <summary>
        /// 返回结果处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async override Task<object> GetTaskResult(WebApiClient.Contexts.ApiActionContext context)
        {
            var txt = await context.ResponseMessage.Content.ReadAsStringAsync();
            if (txt.Contains("status_code"))
            {
                var err = (ResultError)context.HttpApiConfig.JsonFormatter.Deserialize(txt, typeof(ResultError));
                throw new SeniverserException(err.status_code);
            }
            var data = (SeniversResult)context.HttpApiConfig.JsonFormatter.Deserialize(txt, typeof(SeniversResult));
            return ApiRestReulst.True(data);
        }
    }
}

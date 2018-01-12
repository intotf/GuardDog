using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace SeniverseForm
{
    /// <summary>
    /// 查询结果处理
    /// </summary>
    public class SeniversReturnAttribute : ApiReturnAttribute
    {

        public SeniversReturnAttribute()
        {

        }

        /// <summary>
        /// 对接口返回错误码进行转换
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected async override Task<object> GetTaskResult(ApiActionContext context)
        {
            var txt = await context.ResponseMessage.Content.ReadAsStringAsync();
            if (txt.Contains("status_code"))
            {
                var err = (ResultError)context.HttpApiConfig.JsonFormatter.Deserialize(txt, typeof(ResultError));
                throw new SeniverserException(err.status_code);
            }
            var data = context.HttpApiConfig.JsonFormatter.Deserialize(txt, context.ApiActionDescriptor.Return.DataType);
            return data;
        }
    }

}

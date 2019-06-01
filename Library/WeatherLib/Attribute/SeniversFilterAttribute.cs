using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
using WebApiClient;

namespace WeatherLib
{
    /// <summary>
    /// 心知天气接口授权处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class SeniversFilterAttribute : ApiActionFilterAttribute
    {
        /// <summary>
        /// 数据提交前处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            return base.OnBeginRequestAsync(context);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace SeniverseForm
{
    /// <summary>
    /// 获取IP 地址
    /// </summary>
    public class IpReturnAttribute : ApiReturnAttribute
    {

        public IpReturnAttribute()
        {

        }

        /// <summary>
        /// 查找Html 中的IP地址
        /// </summary>
        protected async override Task<object> GetTaskResult(ApiActionContext context)
        {
            var html = await context.ResponseMessage.Content.ReadAsStringAsync();
            var reg = new Regex(@"(?<=\[).*?(?=\])");       // 只找 [] 里面的内容
            //var reg = new Regex(@"\d+\.\d+\.\d+\.\d+");   // 找到IP 地址
            Match m = reg.Match(html);
            return m.Value;
        }
    }
}

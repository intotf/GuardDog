using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Attributes;

namespace SeniverseForm
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
        public override Task OnBeginRequestAsync(WebApiClient.ApiActionContext context)
        {
            var ts = ((long)ToolExtend.GetTimeStamp().TotalSeconds).ToString();
            var uid = "UA8A3D4AEE"; //用户ID
            var ttl = "300";    //有效时长 单位秒
            var key = "oxolqlihh4e4fhtq";   //用户 Key
            var txt = string.Format("ts={0}&ttl={1}&uid={2}", ts, ttl, uid);    //待加密字符串
            var sig = txt.ToBase64hmac(key);    //HMACSHA1 加密
            var dic = new
            {
                ts,
                ttl,
                uid,
                sig
            };
            context.RequestMessage.AddUrlQeury(context.HttpApiConfig.KeyValueFormatter.Serialize(dic));
            return base.OnBeginRequestAsync(context);
        }

    }
}

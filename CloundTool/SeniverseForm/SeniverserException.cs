using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniverseForm
{
    /// <summary>
    /// 自定义异常
    /// </summary>
    public class SeniverserException : Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public StatusCode Code { get; set; }

        /// <summary>
        /// 错误提示信息
        /// </summary>
        public new string Message { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        public SeniverserException(StatusCode code)
        {
            this.Message = code.GetFieldDescription();
            this.Code = code;
        }
    }
}

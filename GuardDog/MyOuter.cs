using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuardDog
{

    /// <summary>
    /// 调试流
    /// </summary>
    class MyWriter : TextWriter
    {
        private readonly SynchronizationContext syncContext;

        private readonly Action<string> syncAction;

        /// <summary>
        /// 获取编码
        /// </summary>
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public MyWriter(SynchronizationContext syncContext, Action<string> syncAction)
        {
            this.syncContext = syncContext;
            this.syncAction = syncAction;
        }


        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void Write(char[] buffer, int index, int count)
        {
            var value = new string(buffer, index, count);
            if (value.EndsWith(Environment.NewLine))
            {
                value = new string(buffer, index, count - Environment.NewLine.Length);
            }

            System.Diagnostics.Debugger.Log(0, null, value);

            this.syncContext.Post((state) => this.syncAction(value), null);
        }
    }
}

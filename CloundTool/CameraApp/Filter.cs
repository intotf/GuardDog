using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkSocket.WebSocket;

namespace CameraApp
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public class Filter : JsonWebSocketFilterAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            Console.WriteLine(filterContext.Exception.Message);
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 连接进来
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnExecuting(ActionContext filterContext)
        {
            Console.WriteLine("{0} {1}连接进来，当前连接数{2}", DateTime.Now, filterContext.Session, filterContext.AllSessions.Count);
        }

        /// <summary>
        /// 断开时
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnExecuted(ActionContext filterContext)
        {
            Console.WriteLine("{0} {1}断开连接，当前连接数{2}", DateTime.Now, filterContext.Session, filterContext.AllSessions.Count);
        }

    }
}

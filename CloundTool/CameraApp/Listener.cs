using NetworkSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraApp
{
    /// <summary>
    /// 连接类
    /// </summary>
    public class Listener
    {
        /// <summary>
        /// WebSocket 连接
        /// </summary>
        public static TcpListener WebSocketListener = new TcpListener();
    }
}

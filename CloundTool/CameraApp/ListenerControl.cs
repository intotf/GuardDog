﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using NetworkSocket;
using NetworkSocket.WebSocket;
using System.Configuration;
using NetworkSocket.Util;
using NetworkSocket.Flex;
using NetworkSocket.Http;

namespace CameraApp
{
    /// <summary>
    /// 监听服务
    /// </summary>
    public class ListenerControl : ServiceControl
    {
        public static TcpListener listener = Listener.WebSocketListener;

        public bool Start(HostControl hostControl)
        {
            var port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            var ower = TcpSnapshot.Snapshot().FirstOrDefault(item => item.Port == port);
            if (ower != null)
            {
                Console.WriteLine("{0} 端口{1}被进程{2}占用 ...", DateTime.Now, port, ower.OwerPid);
                ower.Kill();
            }
            listener.Use<HttpMiddleware>();
            listener.Use<JsonWebSocketMiddleware>().GlobalFilters.Add(new Filter());
            listener.KeepAlivePeriod = TimeSpan.FromSeconds(30);
            listener.Start(port);
            Console.WriteLine("{0} 监听端口{1}成功 ...", DateTime.Now, port);

            return true;
        }

        public bool Stop(HostControl hostControl)
        {

            return true;
        }
    }
}

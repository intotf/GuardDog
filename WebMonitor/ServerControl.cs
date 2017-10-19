using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace WebMonitor
{
    public class ServerControl : ServiceControl
    {
        private CancellationTokenSource cancellSource = new CancellationTokenSource();

        public bool Start(HostControl hostControl)
        {
            Debugger.WriteLine("Web 监控程序,启动成功");
            foreach (var item in Config.Instance.Webs)
            {
                var Monitoring = new Monitoring(item.Name, item.Url);
                Monitoring.StartAsync(this.cancellSource.Token);
            }
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Debugger.WriteLine("Web 监控服务,停止成功.");
            cancellSource.Cancel();
            return true;
        }
    }
}

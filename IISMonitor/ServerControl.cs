using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace IISMonitor
{
    public class ServerControl : ServiceControl
    {
        private CancellationTokenSource cancellSource = new CancellationTokenSource();

        public bool Start(HostControl hostControl)
        {
            Debugger.WriteLine("IIS 监控程序,启动成功");
            var webs = Config.Instance.Webs;
            foreach (var site in webs)
            {
                site.StartAsync(cancellSource.Token);
            }

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Debugger.WriteLine("IIS 监控服务,停止成功.");
            cancellSource.Cancel();
            return true;
        }
    }
}

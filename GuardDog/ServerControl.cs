using Infrastructure.Utility;
using System;
using System.Threading;
using Topshelf;


namespace GuardDog
{
    /// <summary>
    /// 服务控制类
    /// </summary>
    public class ServerControl : ServiceControl
    {
        private CancellationTokenSource cancellSource = new CancellationTokenSource();

        /// <summary>
        /// 服务启动任务
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Start(HostControl hostControl)
        {
            Debugger.WriteLine("看门狗启动成功,开始监控!");
            //服务类监控
            foreach (var item in DogConfig.GetConfigServiceNames())
            {
                var monitor = new ServiceMonitor(item);
                monitor.StartAsync(this.cancellSource.Token);
            }

            //程序类监控
            foreach (var item in DogConfig.GetConfigProcessPaths())
            {
                var monitor = new ProcessMonitor(item);
                monitor.StartAsync(this.cancellSource.Token);
            }
            return true;
        }

        /// <summary>
        /// 服务停止
        /// </summary>
        /// <param name="hostControl"></param>
        /// <returns></returns>
        public bool Stop(HostControl hostControl)
        {
            Debugger.WriteLine("看门狗停止成功.");
            cancellSource.Cancel();
            return true;
        }
    }
}

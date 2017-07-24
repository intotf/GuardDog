using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Utility;

namespace GuardDog
{
    /// <summary>
    /// 抽象实现监控
    /// </summary>
    public abstract class MonitorBase : IMonitor
    {
        /// <summary>
        /// 服务/进程名称
        /// </summary>
        public abstract string TargetName { get; }

        /// <summary>
        /// 运行服务
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async void StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                throw new ArgumentNullException();
            }
            //执行监测并自动启动服务
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var state = await this.CheckTargetAsync();
                    Debugger.WriteLine("{0} 当前状态为 {1}", this.TargetName, (state ? "正常" : "未启动"));
                    if (!state)
                    {
                        state = await this.RunTargetAsync();
                        Debugger.WriteLine("启动 {0} {1},请检查服务/程序是否存在.", this.TargetName, state ? "成功" : "失败");
                        //this.NotifyCenterAsync("检测到目标状态不正常，修复" + (state ? "成功" : "失败"));
                    }
                }
                catch (Exception ex)
                {
                    Debugger.WriteLine("{0} =>{1}", this.TargetName, ex.Message);
                }
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }


        /// <summary>
        /// 获取监控的对象是否正常
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> CheckTargetAsync();

        /// <summary>
        /// 启动或运行监控的对象
        /// </summary>
        /// <returns></returns>
        protected abstract Task<bool> RunTargetAsync();
    }
}

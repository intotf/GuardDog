using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Utility;
using System.Collections.Generic;

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
        /// 服务/进程
        /// </summary>
        public abstract string SystemName { get; }

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
                        var msg = string.Format("检测到 {0} 状态不正常，修复 {1}", this.TargetName, (state ? "正常" : "未启动"));
                        Debugger.WriteLine(msg);
                        await MailSend.SendMail(msg, this.TargetName, this.SystemName);
                        await HttpSend.Send(msg, this.TargetName, this.SystemName);
                    }
                }
                catch (Exception ex)
                {
                    Debugger.WriteLine("{0} =>{1}", this.TargetName, ex.Message);
                    MailSend.SendMail(ex.Message, this.TargetName, this.SystemName);
                    HttpSend.Send(ex.Message, this.TargetName, this.SystemName);
                }
                await Task.Delay(TimeSpan.FromSeconds(DogConfig.Instance.IntervalTime.Seconds));
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

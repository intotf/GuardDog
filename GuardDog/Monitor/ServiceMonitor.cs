using System.ServiceProcess;
using System.Threading.Tasks;

namespace GuardDog
{
    /// <summary>
    /// 服务类监控
    /// </summary>
    public class ServiceMonitor : MonitorBase
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        private readonly string serviceName;

        public override string SystemName
        {
            get { return "服务"; }
        }

        /// <summary>
        /// 控制器
        /// </summary>
        private readonly ServiceController service;

        public override string TargetName
        {
            get
            {
                return this.serviceName;
            }
        }

        /// <summary>
        /// 服务监控构造函数
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        public ServiceMonitor(string serviceName)
        {
            this.serviceName = serviceName;
            this.service = new ServiceController(serviceName);
        }

        /// <summary>
        /// 检测服务状态
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> CheckTargetAsync()
        {
            this.service.Refresh();
            var state = this.service.Status != ServiceControllerStatus.Stopped;
            return Task.FromResult(state);
        }

        /// <summary>
        /// 运行服务
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> RunTargetAsync()
        {
            this.service.Start();
            return Task.FromResult(true);
        }


    }
}

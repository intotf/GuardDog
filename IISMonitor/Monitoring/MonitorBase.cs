using Infrastructure.Utility;
using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IISMonitor
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class MonitorBase : IMonitoring
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public abstract string SiteName { get; set; }

        /// <summary>
        /// 当前站点
        /// </summary>
        private Site _Site { get; set; }

        /// <summary>
        /// 当前站点对应的应用池
        /// </summary>
        private ApplicationPool _Pool { get; set; }

        /// <summary>
        /// 连接IIS
        /// </summary>
        private void OpenIIS()
        {
            using (var iis = ServerManager.OpenRemote("localhost"))
            {
                _Site = iis.Sites.Where(item => item.Name == this.SiteName).FirstOrDefault();
                var pool = _Site.Applications.FirstOrDefault();
                if (pool != null)
                {
                    _Pool = iis.ApplicationPools.Where(item => item.Name == pool.ApplicationPoolName).FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// 开始监控
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async void StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                throw new ArgumentNullException();
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    this.OpenIIS();
                    if (_Site != null && _Site.State != ObjectState.Started)
                    {
                        Loger.Today("Log").AppendLine(DateTime.Now).AppendLine(_Site.Name + "站点被停止,正在启动.").Save();
                        _Site.Start();
                    }

                    if (_Pool != null && _Pool.State != ObjectState.Started)
                    {
                        Loger.Today("Log").AppendLine(DateTime.Now).AppendLine(_Pool.Name + "应用池被停止,正在启动.").Save();
                        _Pool.Start();
                    }
                }
                catch (Exception ex)
                {
                    Loger.Today("Log").AppendLine(DateTime.Now).AppendLine(ex.Message).Save();
                }
                await Task.Delay(Config.Instance.IntervalTime * 60 * 1000);
            }
        }
    }
}

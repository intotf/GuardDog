using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IISMonitor
{
    public interface IMonitoring
    {
        /// <summary>
        /// 开始监控
        /// </summary>
        void StartAsync(CancellationToken cancellationToken);
    }
}

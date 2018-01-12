using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.Administration;
using System.Threading;

namespace IISMonitor
{
    public class Monitoring
    {
        private ApplicationPool appPool { get; set; }

        private Site webSize { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Monitoring(string appName, string webName)
        {
            this.Name = appName;
            this.OpenIIS();
        }

        private void OpenIIS()
        {
            using (var iis = ServerManager.OpenRemote("localhost"))
            {
                webSize = iis.Sites.Where(item => item.Name == this.Name).FirstOrDefault();
                //appPool = iis.ApplicationPools.Where(item => item.Name == this.ApplicationName).FirstOrDefault();
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                throw new ArgumentNullException();
            }

            //执行监测并自动启动服务
            while (!cancellationToken.IsCancellationRequested)
            {
                if (appPool.State != ObjectState.Started)
                {
                    appPool.Start();
                }

                if (webSize.State != ObjectState.Started)
                {
                    webSize.Start();
                }
                await Task.Delay(1000);
            }
        }

        public async Task SendMailAsync()
        {
            throw new NotImplementedException();
        }

    }
}

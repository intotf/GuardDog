using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure;
using WebMonitor.Push;
using System.Collections.Concurrent;

namespace WebMonitor
{
    /// <summary>
    /// 网站监控
    /// </summary>
    public class Monitoring : Webs
    {
        /// <summary>
        /// Web列表
        /// </summary>
        private readonly static ConcurrentDictionary<Uri, bool> webList = new ConcurrentDictionary<Uri, bool>();


        public Monitoring(string name, Uri url)
        {
            this.Name = name;
            this.Url = url;
        }

        /// <summary>
        /// 检测站点是否正常
        /// </summary>
        /// <returns></returns>
        public async void StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken == null)
            {
                throw new ArgumentNullException();
            }

            //执行监测并自动启动服务
            while (!cancellationToken.IsCancellationRequested)
            {

                using (var client = new HttpClient(Config.Instance.TimeOut * 1000))
                {
                    var exMsg = "正常.";
                    string result = string.Empty;
                    try
                    {
                        result = await client.DownloadStringTaskAsync(this.Url);
                        var state = webList.GetOrAdd(this.Url, true);
                        webList.AddOrUpdate(this.Url, true, (key, oldState) => true);
                        if (!state)
                        {
                            exMsg = "从异常中恢复";
                            Debugger.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                            await SendEmail(exMsg);
                        }
                        else
                        {
                            Debugger.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                            await TaskDelay();
                        }
                        continue;
                    }
                    catch (Exception ex)
                    {
                        exMsg = ex.Message;
                    }
                    webList.AddOrUpdate(this.Url, false, (key, oldState) => false);
                    Debugger.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                    await SendEmail(exMsg);
                    await TaskDelay();
                    continue;
                }
            }
        }

        /// <summary>
        /// 检测完后等待
        /// </summary>
        /// <returns></returns>
        private async Task TaskDelay()
        {
            await Task.Delay(TimeSpan.FromMinutes(Config.Instance.IntervalTime));
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private async Task SendEmail(string msg)
        {
            var sendModel = new MailContent(this.Name, this.Url.ToString(), msg);
            var pubs = new BastPush(sendModel);
            await pubs.SendAsync();
            await TaskDelay();
        }
    }
}

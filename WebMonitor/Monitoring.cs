using Infrastructure.Utility;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using WebMonitor.Push;

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
        private readonly static ConcurrentDictionary<Uri, Webs> webList = new ConcurrentDictionary<Uri, Webs>();

        public Monitoring(string name, Uri url)
        {
            this.Name = name;
            this.Url = url;
            this.Attempts = 0;
            this.State = true;
            this.NoticeInterval = 1;
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

            var config = Config.Instance;

            var client = new HttpClient(config.TimeOut * 1000);
            var ping = new Ping();

            //执行监测并自动启动服务
            while (!cancellationToken.IsCancellationRequested)
            {
                #region 检测当前环境网格
                try
                {
                    var pingSatet = await ping.SendPingAsync("www.baidu.com", 1000);
                    if (pingSatet.Status != IPStatus.Success)
                    {
                        this.Attempts = 0;
                        Console.WriteLine("{0} {1}", DateTime.Now, "当前网络连接失败,或网络不稳定.");
                        await TaskDelay();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("{0} {1} {2}", DateTime.Now, "当前网络连接失败", ex.Message);
                    Thread.Sleep(5000);
                    continue;
                }
                #endregion

                var exMsg = "正常";
                string result = string.Empty;
                //var model = new Webs() { Name = this.Name, Url = this.Url, Attempts = 0, State = true };
                Stopwatch sw = new Stopwatch();  //运行时间
                try
                {
                    sw.Reset();
                    sw.Start();
                    result = await client.DownloadStringTaskAsync(this.Url);
                    sw.Stop();
                    this.Attempts = 0;
                    this.LastAlarmTime = null;
                    this.NoticeInterval = 5;
                    if (!this.State && this.Attempts >= config.Attempts)
                    {
                        exMsg = "从异常中恢复,访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                        Console.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                        await SendEmail(exMsg);
                    }
                    else
                    {
                        exMsg += ",访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                        Console.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                        await TaskDelay();
                    }
                    //var oldModel = webList.GetOrAdd(this.Url, this);
                    //if (!oldModel.State && oldModel.Attempts >= config.Attempts)
                    //{
                    //    exMsg = "从异常中恢复,访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                    //    Console.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                    //    await SendEmail(exMsg);
                    //}
                    //else
                    //{
                    //    exMsg += ",访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                    //    Console.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                    //    await TaskDelay();
                    //}
                    //webList.AddOrUpdate(this.Url, this, (key, newModel) => this);
                    continue;
                }
                catch (WebException ex)
                {
                    exMsg = ex.Message;
                }
                exMsg += ",访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                this.State = false;
                //先查询是否有记录
                //var exModel = webList.GetOrAdd(this.Url, model);
                this.Attempts += 1;
                this.State = false;

                if (this.Attempts < config.Attempts)
                {
                    Console.WriteLine("{0} {1} {2} 正在重试 {3}!", this.Url.ToString(), this.Name, exMsg, this.Attempts);
                    await Task.Delay(2000);
                    continue;
                }

                this.Attempts = 0;
                //第一次时实发送邮件
                if (!this.LastAlarmTime.HasValue)
                {
                    this.LastAlarmTime = DateTime.Now;
                    await SendEmail(exMsg);
                }
                else
                {
                    //第二次发送判断与第一次的间隔时间
                    if (DateTime.Now.Subtract(this.LastAlarmTime.Value).Minutes > this.NoticeInterval)
                    {
                        this.LastAlarmTime = DateTime.Now;
                        this.NoticeInterval = this.NoticeInterval * 2;
                        await SendEmail(exMsg);
                    }
                }
                await TaskDelay();
                continue;
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
        /// <param name="IsForce">是否强制发送</param>
        /// <returns></returns>
        private async Task SendEmail(string msg)
        {
            Console.WriteLine("{0} {1} {2} 发送邮件!", this.Url.ToString(), this.Name, msg);
            var sendModel = new MailContent(this.Name, this.Url.ToString(), msg);
            var pubs = new BastPush(sendModel);
            await pubs.SendAsync();
        }
    }
}

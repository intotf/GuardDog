﻿using Infrastructure.Utility;
using System;
using System.Collections.Concurrent;
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

            //执行监测并自动启动服务
            while (!cancellationToken.IsCancellationRequested)
            {
                var ping = new Ping();
                try
                {
                    var pingSatet = await ping.SendPingAsync("www.baidu.com", 1000);
                    if (pingSatet.Status != IPStatus.Success)
                    {
                        Debugger.WriteLine("{0} {1}", DateTime.Now, "当前网络连接失败,或网络不稳定.");
                        await TaskDelay();
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Debugger.WriteLine("{0} {1} ", DateTime.Now, ex.Message);
                    continue;
                }
                using (var client = new HttpClient(config.TimeOut * 1000))
                {
                    var exMsg = "正常";
                    string result = string.Empty;
                    var model = new Webs() { Name = this.Name, Url = this.Url, Attempts = 0, State = true };
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();  //运行时间
                    try
                    {
                        sw.Reset();
                        sw.Start();
                        result = await client.DownloadStringTaskAsync(this.Url);
                        sw.Stop();
                        var oldModel = webList.GetOrAdd(this.Url, model);
                        if (!oldModel.State && oldModel.Attempts >= config.Attempts)
                        {
                            exMsg = "从异常中恢复,访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                            Debugger.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                            await SendEmail(exMsg);
                        }
                        else
                        {
                            exMsg += ",访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                            Debugger.WriteLine("{0} {1} {2}", this.Url.ToString(), this.Name, exMsg);
                            await TaskDelay();
                        }
                        webList.AddOrUpdate(this.Url, model, (key, newModel) => model);
                        continue;
                    }
                    catch (WebException ex)
                    {
                        exMsg = ex.Message;
                    }
                    exMsg += ",访问耗时 " + sw.ElapsedMilliseconds + "ms.";
                    model.State = false;
                    //先查询是否有记录
                    var exModel = webList.GetOrAdd(this.Url, model);
                    exModel.Attempts += 1;
                    exModel.State = false;
                    if (exModel.Attempts < config.Attempts)
                    {
                        Debugger.WriteLine("{0} {1} {2} 正在重试 {3}!", this.Url.ToString(), this.Name, exMsg, exModel.Attempts);
                        await Task.Delay(2000);
                        continue;
                    }
                    webList.AddOrUpdate(this.Url, exModel, (key, oldState) => exModel);

                    Debugger.WriteLine("{0} {1} {2} 发送邮件!", this.Url.ToString(), this.Name, exMsg);
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

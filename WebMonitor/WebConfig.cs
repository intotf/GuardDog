using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Infrastructure.Utility;
using Infrastructure;

namespace WebMonitor
{
    /// <summary>
    /// 监控站点配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 配置文件路径
        /// </summary>
        private static readonly string configFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "WebSites.xml";

        public static Config Instance = GetConfig();

        public Config NewConfig()
        {
            return GetConfig();
        }

        /// <summary>
        /// 站点访问超时时间,单位秒
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 轮训间隔时间，单位分钟
        /// </summary>
        public int IntervalTime { get; set; }

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public bool IsSendMail { get; set; }

        /// <summary>
        /// 尝试次数
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// 监控站点列表
        /// </summary>
        public IEnumerable<Webs> Webs { get; set; }

        /// <summary>
        /// 异常消息邮箱发送
        /// </summary>
        public IEnumerable<string> Emails { get; set; }

        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <param name="xmlPath"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static void SavaConfig(Config model)
        {
            //获取根节点对象
            XDocument document = new XDocument();
            XElement root = new XElement("Root");

            XElement configEle = new XElement("WebSites");
            configEle.SetAttributeValue("TimeOut", model.TimeOut);
            configEle.SetAttributeValue("IsSendMail", model.IsSendMail);
            configEle.SetAttributeValue("IntervalTime", model.IntervalTime);
            configEle.SetAttributeValue("Attempts", model.Attempts);
            foreach (var item in model.Webs)
            {
                XElement webEle = new XElement("Web");
                webEle.SetAttributeValue("Name", item.Name);
                webEle.SetAttributeValue("Url", item.Url);
                configEle.Add(webEle);
            }
            root.Add(configEle);

            var mailEle = new XElement("Emails");
            foreach (var item in model.Emails)
            {
                XElement eEle = new XElement("Item");
                eEle.SetAttributeValue("Mail", item);
                mailEle.Add(eEle);
            }
            root.Add(mailEle);

            root.Save(configFilePath);
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <returns></returns>
        private static Config GetConfig()
        {
            var model = new Config();
            var XDoc = XDocument.Load(configFilePath);
            var configEle = XDoc.Root.Element("WebSites");
            model.TimeOut = configEle.Attribute("TimeOut").Value.ToInt32(10);
            model.IsSendMail = configEle.Attribute("IsSendMail").Value.ToBool(false);
            model.IntervalTime = configEle.Attribute("IntervalTime").Value.ToInt32(30);
            model.Attempts = configEle.Attribute("Attempts").Value.ToInt32(3);
            model.Webs = Config.GetWebs(configEle.Elements("Web"));

            var mailEle = XDoc.Root.Element("Emails").Elements("Item");
            model.Emails = Config.GetEmails(mailEle);

            return model;
        }

        /// <summary>
        /// 获取所有Web
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Webs> GetWebs(IEnumerable<XElement> ele)
        {
            foreach (var item in ele)
            {
                var name = item.Attribute("Name").Value;
                var url = item.Attribute("Url").Value;
                yield return new Webs(name, url);
            }
        }

        /// <summary>
        /// 获取所有Web
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetEmails(IEnumerable<XElement> ele)
        {
            foreach (var item in ele)
            {
                var mail = item.Attribute("Mail").Value;
                yield return mail;
            }
        }
    }

    /// <summary>
    /// 站点
    /// </summary>
    public class Webs
    {
        public Webs()
        {

        }

        /// <summary>
        /// 构建对象
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        public Webs(string name, string url)
        {
            this.Name = name;
            this.Url = new Uri(url);
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点访问地址
        /// </summary>
        public Uri Url { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }

        /// <summary>
        /// 异常次数
        /// </summary>
        public int Attempts { get; set; }
    }

    /// <summary>
    /// Webs 比较器
    /// </summary>
    public class WebsComparer : IEqualityComparer<Webs>
    {
        public bool Equals(Webs x, Webs y)
        {
            return true;
        }

        public int GetHashCode(Webs obj)
        {
            return obj.Url.GetHashCode();
        }
    }
}

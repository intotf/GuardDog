using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Infrastructure.Utility;
using Infrastructure;

namespace IISMonitor
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

        public static Config Instance = new Config();

        public Config()
        {
            this.GetConfig();
        }

        /// <summary>
        /// 轮训间隔时间，单位分钟
        /// </summary>
        public int IntervalTime { get; set; }

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public bool IsSendMail { get; set; }

        /// <summary>
        /// 监控站点列表
        /// </summary>
        public IEnumerable<SiteMonitor> Webs { get; set; }

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
            configEle.SetAttributeValue("IsSendMail", model.IsSendMail);
            configEle.SetAttributeValue("IntervalTime", model.IntervalTime);
            foreach (var item in model.Webs)
            {
                XElement webEle = new XElement("Web");
                webEle.SetAttributeValue("Name", item.SiteName);
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
        private Config GetConfig()
        {
            //var model = new Config();
            var XDoc = XDocument.Load(configFilePath);
            var configEle = XDoc.Root.Element("WebSites");
            this.IsSendMail = configEle.Attribute("IsSendMail").Value.ToBool(false);
            this.IntervalTime = configEle.Attribute("IntervalTime").Value.ToInt32(30);
            this.Webs = Config.GetWebs(configEle.Elements("Web"));

            var mailEle = XDoc.Root.Element("Emails").Elements("Item");
            this.Emails = Config.GetEmails(mailEle);

            return this;
        }

        /// <summary>
        /// 获取所有Web
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<SiteMonitor> GetWebs(IEnumerable<XElement> ele)
        {
            foreach (var item in ele)
            {
                var name = item.Attribute("Name").Value;
                yield return new SiteMonitor(name);
            }
        }

        /// <summary>
        /// 获取所有Web
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> GetEmails(IEnumerable<XElement> ele)
        {
            foreach (var item in ele)
            {
                var mail = item.Attribute("Mail").Value;
                yield return mail;
            }
        }
    }
}

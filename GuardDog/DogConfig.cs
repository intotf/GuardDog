using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace GuardDog
{
    /// <summary>
    /// 获取系统配置文件
    /// </summary>
    public class DogConfig : ConfigurationSection
    {
        public static readonly DogConfig Instance = ConfigurationManager.GetSection("MonitorConfig") as DogConfig;

        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetConfigServiceNames()
        {
            foreach (var node in Instance.Services.Cast<ItemNode>())
            {
                yield return node.Name;
            }
        }

        /// <summary>
        /// 获取所有程序
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetConfigProcessPaths()
        {
            foreach (var node in Instance.Process.Cast<ItemNode>())
            {
                yield return node.Path;
            }
        }

        /// <summary>
        /// 获取所有邮件
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetConfigMails()
        {
            foreach (var node in Instance.Mails.Cast<ItemNode>())
            {
                yield return node.Name;
            }
        }

        /// <summary>
        /// 配置名称
        /// </summary>
        [ConfigurationProperty("Name", DefaultValue = "")]
        public string Name
        {
            get
            {
                return (string)this["Name"];
            }
        }

        /// <summary>
        /// 是否发送邮件
        /// </summary>
        [ConfigurationProperty("IsSend", DefaultValue = false)]
        public bool IsSend
        {
            get
            {
                return (bool)this["IsSend"];
            }
        }

        /// <summary>
        /// Http 通知中心
        /// </summary>
        [ConfigurationProperty("HttpNotify")]
        public HttpNotify HttpNotify
        {
            get
            {
                return (HttpNotify)this["HttpNotify"];
            }
        }

        /// <summary>
        /// 轮训设置
        /// </summary>
        [ConfigurationProperty("IntervalTime")]
        public IntervalTime IntervalTime
        {
            get
            {
                return (IntervalTime)this["IntervalTime"];
            }
        }

        /// <summary>
        /// 服务节点集合
        /// </summary>
        [ConfigurationProperty("Services")]
        public ItemNodeCollection Services
        {
            get
            {
                return (ItemNodeCollection)this["Services"];
            }
        }

        /// <summary>
        /// 进程节点集合
        /// </summary>
        [ConfigurationProperty("Process")]
        public ItemNodeCollection Process
        {
            get
            {
                return (ItemNodeCollection)this["Process"];
            }
        }

        /// <summary>
        /// 邮件发送
        /// </summary>
        [ConfigurationProperty("Mails")]
        public ItemNodeCollection Mails
        {
            get
            {
                return (ItemNodeCollection)this["Mails"];
            }
        }

        /// <summary>
        /// 网站监控列表
        /// </summary>
        [ConfigurationProperty("WebSites")]
        public ItemNodeCollection WebSites
        {
            get
            {
                return (ItemNodeCollection)this["WebSites"];
            }
        }
    }

    /// <summary>
    /// 轮训时长
    /// </summary>
    public class IntervalTime : ConfigurationElement
    {
        /// <summary>
        /// 轮训时长，单位位秒
        /// </summary>
        [ConfigurationProperty("Seconds", DefaultValue = 10)]
        public int Seconds
        {
            get
            {
                return (int)base["Seconds"];
            }
        }
    }

    /// <summary>
    /// 收集中心
    /// </summary>
    public class HttpNotify : ConfigurationElement
    {
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty("URL", DefaultValue = null)]
        public Uri URL
        {
            get
            {
                return (Uri)base["URL"];
            }
        }

        /// <summary>
        /// Auth参数
        /// </summary>
        [ConfigurationProperty("Auth", DefaultValue = "")]
        public string Auth
        {
            get
            {
                return (string)base["Auth"];
            }
        }

        /// <summary>
        /// 异常 http通知间隔时间，单位分钟
        /// </summary>
        [ConfigurationProperty("MaxInterval", DefaultValue = 5)]
        public int MaxInterval
        {
            get
            {
                return (int)base["MaxInterval"];
            }
        }
    }


    /// <summary>
    /// 节点
    /// </summary>
    public class ItemNode : ConfigurationElement
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [ConfigurationProperty("Name", DefaultValue = "")]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        [ConfigurationProperty("Path", DefaultValue = "")]
        public string Path
        {
            get
            {
                return (string)base["Path"];
            }
        }
    }

    /// <summary>
    /// 监控节点集合
    /// </summary>
    public class ItemNodeCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ItemNode();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var ele = element as ItemNode;
            return ele.Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "Item";
            }
        }
    }
}

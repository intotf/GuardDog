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
        /// 收集中心URL
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
        /// 收集中心
        /// </summary>
        [ConfigurationProperty("Center")]
        public Center Center
        {
            get
            {
                return (Center)this["Center"];
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
    }

    /// <summary>
    /// 收集中心
    /// </summary>
    public class Center : ConfigurationElement
    {
        /// <summary>
        /// 地址
        /// </summary>
        [ConfigurationProperty("URL", DefaultValue = "")]
        public string URL
        {
            get
            {
                return (string)base["URL"];
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

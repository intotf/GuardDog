using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IISMonitor
{
    /// <summary>
    /// 站点监控
    /// </summary>
    public class SiteMonitor : MonitorBase, IEqualityComparer<SiteMonitor>
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public override string SiteName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="SizeName"></param>
        public SiteMonitor(string SizeName)
        {
            this.SiteName = SizeName;
        }

        public bool Equals(SiteMonitor x, SiteMonitor y)
        {
            return true;
        }

        public int GetHashCode(SiteMonitor obj)
        {
            return obj.SiteName.GetHashCode();
        }
    }
}

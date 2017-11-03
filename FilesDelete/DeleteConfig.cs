using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesDelete
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class DeleteConfig
    {
        public static DeleteConfig instance = new DeleteConfig();

        /// <summary>
        /// 当前时间
        /// </summary>
        public DateTime now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 删除文件的目录
        /// </summary>
        public string dir
        {
            get { return ConfigurationManager.AppSettings["Dir"]; }
        }

        /// <summary>
        /// 删除的文件后缀
        /// </summary>
        public string[] filter
        {
            get
            {
                return ConfigurationManager.AppSettings["Filter"].ToString().Split('|');
            }
        }

        /// <summary>
        /// 是否真实删除
        /// </summary>
        public bool delFalg
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["Delete"]);
            }
        }

        /// <summary>
        /// 是否删除空文件夹
        /// </summary>
        public bool delEmpty
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["DeleteEmptyDir"]);
            }
        }

        /// <summary>
        /// 轮训间隔时间,单位分钟
        /// </summary>
        public int IntervalTime
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IntervalTime"]);
            }
        }

        /// <summary>
        /// 删除在Days天以前创建的文件
        /// </summary>
        public int days
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["Days"]);
            }
        }

    }
}

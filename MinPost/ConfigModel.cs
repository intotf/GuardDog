using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MinPost
{
    public class ConfigModel
    {
        /// <summary>
        /// 默认提交语言
        /// </summary>
        public static string DefaultLanguage
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultLanguage"];
            }
        }

        /// <summary>
        /// 默认提交方式
        /// </summary>
        public static string DefaultPostType
        {
            get
            {
                return ConfigurationManager.AppSettings["DefaultPostType"];
            }
        }
    }
}

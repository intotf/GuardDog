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
                var language = ConfigurationManager.AppSettings["DefaultLanguage"];
                return language.IsNullOrEmpty() ? LanguageType.UTF8.GetFieldDisplay() : language;
            }
        }

        /// <summary>
        /// 默认提交方式
        /// </summary>
        public static string DefaultPostType
        {
            get
            {
                var postType = ConfigurationManager.AppSettings["DefaultPostType"];
                return postType.IsNullOrEmpty() ? PostType.Post.GetFieldDisplay() : postType;
            }
        }
    }
}

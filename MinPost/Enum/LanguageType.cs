using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MinPost
{
    /// <summary>
    /// 提交内容编码
    /// </summary>
    public enum LanguageType
    {
        /// <summary>
        /// UTF-8
        /// </summary>
        [Display(Name = "UTF-8")]
        UTF8 = 0,

        /// <summary>
        /// GB2312
        /// </summary>
        [Display(Name = "GB2312")]
        GB2312 = 1,
    }
}

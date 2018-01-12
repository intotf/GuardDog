using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MinPost
{
    /// <summary>
    /// 提交类型
    /// </summary>
    public enum PostType
    {
        /// <summary>
        /// Post 提交
        /// </summary>
        [Display(Name = "Post")]
        Post = 0,

        /// <summary>
        /// Get 提交
        /// </summary>
        [Display(Name = "Get")]
        Get = 1
    }
}

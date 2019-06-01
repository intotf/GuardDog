using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinPost
{
    /// <summary>
    /// 提交参数实体
    /// </summary>
    [Serializable]
    public class ParameterModel
    {
        /// <summary>
        /// 提交的 Url 地址
        /// </summary>
        public Uri postUrl { get; set; }

        /// <summary>
        /// 提交的类型
        /// </summary>
        public PostType postType { get; set; }

        /// <summary>
        /// 提交编码
        /// </summary>
        public LanguageType langType { get; set; }

        /// <summary>
        /// 提交参数体
        /// </summary>
        public string postBody { get; set; }

        /// <summary>
        /// 提交的文件列表
        /// </summary>
        public List<string> fileList { get; set; }

        /// <summary>
        /// 提交请求头Headers
        /// </summary>
        public Dictionary<string, string> HeadersDic { get; set; }

        /// <summary>
        /// 是否二进制提交
        /// </summary>
        public bool IsBinary { get; set; }

        /// <summary>
        /// 是否为BodyRaw 方式提交
        /// </summary>
        public bool IsBodyRaw { get; set; }
    }

    /// <summary>
    /// 文件树
    /// </summary>
    public class FilesTree
    {
        /// <summary>
        /// 文件名称/目录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; set; }

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; set; }

        /// <summary>
        /// Tree 的名称
        /// </summary>
        public string Title { get; set; }
    }
}

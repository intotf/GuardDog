using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesDelete
{
    public static class BigDirectory
    {
        private static DeleteConfig config = DeleteConfig.instance;

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DelDirectory(string filePath)
        {
            var file = new FileInfo(filePath);
            if (Filefilter(file))
            {
                File.Delete(filePath);
            }
            else
            {
                Console.WriteLine("跳过 {1} {0}", file.CreationTime, file.Name);
            }
        }

        /// <summary>
        /// 判断文件是否可删除
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static bool Filefilter(FileInfo file)
        {
            return file.FullName.Substring(file.FullName.Length - 2, 2) != ".." &&
                           config.filter.Contains(file.Extension) &&
                           file.CreationTime.AddDays(config.days) > config.now;
        }

    }
}

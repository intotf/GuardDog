using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace FilesDelete
{
    public class Deleter
    {
        private static Timer timer;
        private static string dir = ConfigurationManager.AppSettings["Dir"];
        private static string filter = ConfigurationManager.AppSettings["Filter"];
        private static bool flag = bool.Parse(ConfigurationManager.AppSettings["Delete"]);
        private static int days = int.Parse(ConfigurationManager.AppSettings["Days"]);
        private static int delNum = 0;     //删除文件数
        private static int ignoreNum = 0;    //忽略文件数
        private static Stopwatch sw = new Stopwatch();  //运行时间

        /// <summary>
        /// 初始化服务
        /// </summary>
        public static void Init()
        {
            if (Deleter.timer == null)
            {
                TimeSpan dueTime = DateTime.Today.AddDays(1).Subtract(DateTime.Now);
                TimeSpan period = TimeSpan.FromDays(1);
                Deleter.timer = new Timer((state) =>
                {
                    Deleter.SeachFile();
                }, null, dueTime, period);
            }
        }

        /// <summary>
        /// 目录下所有文件及目录
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<WinFile> getFiles(string dir, string filter)
        {
            var fileList = WinFile.GetFiles(dir, filter).ToList();
            return fileList;
        }


        public static void DeleteFile(List<WinFile> files, string filter)
        {
            DateTime now = DateTime.Now;
            TimeSpan t = TimeSpan.FromDays((double)days);
            foreach (WinFile current in files)
            {
                if (current.Attributes == FileAttributes.Directory)
                {
                    DeleteFile(getFiles(current.FileName, filter), filter);
                }
                else
                {
                    TimeSpan t2 = now.Subtract(current.CreationTime);
                    if (t2 > t && filter.Contains(Path.GetExtension(current.FileName)))
                    {
                        if (flag)
                        {
                            current.Delete();
                        }
                        delNum++;
                        Console.WriteLine("删除 {0} {1} ", current.CreationTime, Path.GetFileName(current.FileName));
                    }
                    else
                    {
                        ignoreNum++;
                        Console.WriteLine("跳过 {0} {1} ", current.CreationTime, Path.GetFileName(current.FileName));
                    }
                }
                Console.Title = string.Format("删除{0} 跳过{1},运行时长 {2} ms.", delNum, ignoreNum, sw.ElapsedMilliseconds);
            }

        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public static void SeachFile()
        {
            sw.Reset();
            sw.Start();
            var fileList = getFiles(dir, filter);
            DeleteFile(fileList, filter);
            sw.Stop();
            Console.WriteLine("{0} 执行完毕.总共耗时 {1} ms.", DateTime.Now, sw.ElapsedMilliseconds);
        }
    }
}

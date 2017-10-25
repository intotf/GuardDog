using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace FilesDelete
{
    public class Deleter
    {
        private static Timer timer;
        private static readonly string dir = ConfigurationManager.AppSettings["Dir"];
        private static readonly string[] filter = ConfigurationManager.AppSettings["Filter"].ToString().Split('|');
        private static readonly bool delFalg = bool.Parse(ConfigurationManager.AppSettings["Delete"]);
        private static readonly int days = int.Parse(ConfigurationManager.AppSettings["Days"]);
        private static readonly bool delEmpty = bool.Parse(ConfigurationManager.AppSettings["DeleteEmptyDir"]);
        private static int delNum = 0;     //删除文件数
        private static int ignoreNum = 0;    //忽略文件数
        private static Stopwatch sw = new Stopwatch();  //运行时间

        /// <summary>
        /// 初始化服务
        /// 
        /// </summary>
        public static void Init()
        {
            if (Deleter.timer == null)
            {
                var IntervalTime = int.Parse(ConfigurationManager.AppSettings["IntervalTime"]);
                TimeSpan dueTime = DateTime.Now.AddMinutes(IntervalTime).Subtract(DateTime.Now);
                TimeSpan period = TimeSpan.FromMinutes(IntervalTime);
                Deleter.timer = new Timer((state) =>
                {
                    Deleter.SeachFile();
                }, null, dueTime, period);
            }
        }

        /// <summary>
        /// 目录下所有文件及目录
        /// </summary>
        /// <param name="dir">目录名称</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<WinFile> getFiles(string dir)
        {
            //不过滤后缀查询,否则无法递归查询子目录
            var fileList = WinFile.GetFiles(dir, null).ToList();
            return fileList;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="files">需要删除的文件列表</param>
        /// <param name="filter">过滤的文件后缀</param>
        public static void DeleteFile(List<WinFile> files)
        {
            DateTime now = DateTime.Now;
            TimeSpan t = TimeSpan.FromDays((double)days);
            foreach (WinFile current in files)
            {
                if (current.Attributes == FileAttributes.Directory)
                {
                    var fileList = getFiles(current.FileName);
                    if (fileList.Count() > 0)
                    {
                        DeleteFile(fileList);
                    }
                    else
                    {
                        if (delEmpty)
                        {
                            Directory.Delete(current.FileName);
                            delNum++;
                            Console.WriteLine("删除 空文件夹 {0}", current.FileName);
                        }
                    }
                }
                else
                {
                    TimeSpan t2 = now.Subtract(current.CreationTime);
                    if (t2 > t && filter.Contains(Path.GetExtension(current.FileName)))
                    {
                        if (delFalg)
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
        /// 执行查询文件并做删除
        /// </summary>
        /// <returns></returns>
        public static void SeachFile()
        {
            sw.Reset();
            sw.Start();
            var fileList = getFiles(dir);
            DeleteFile(fileList);
            sw.Stop();
            Console.WriteLine("{0} 执行完毕.总共耗时 {1} ms.", DateTime.Now, sw.ElapsedMilliseconds);
            delNum = 0;
            ignoreNum = 0;
        }
    }
}

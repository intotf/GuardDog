using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DelBigDirectory
{
    public class Deleter
    {
        private static DeleteConfig config = DeleteConfig.instance;
        private static Timer timer;
        private static int delNum = 0;     //删除文件数
        private static int ignoreNum = 0;    //忽略文件数
        private static Stopwatch sw = new Stopwatch();  //运行时间
        //待删除文件
        private static List<string> delFiles = new List<string>();

        /// <summary>
        /// 初始化服务
        /// 
        /// </summary>
        public static void Init()
        {
            if (Deleter.timer == null)
            {
                TimeSpan dueTime = DateTime.Now.AddMinutes(config.IntervalTime).Subtract(DateTime.Now);
                TimeSpan period = TimeSpan.FromMinutes(config.IntervalTime);
                Deleter.timer = new Timer((state) =>
                {
                    Deleter.DelAllFile();
                }, null, dueTime, period);
            }
        }

        /// <summary>
        /// 删除所有文件
        /// </summary>
        public static void DelAllFile()
        {
            sw.Restart();
            var dir = new System.IO.DirectoryInfo(config.dir);
            var files = dir.EnumerateFiles("*.*");
            foreach (var item in files)
            {
                DelFile(item);
            }
            //WinFile.SearchByDel(config.dir);
            sw.Stop();
            Console.WriteLine("{0} 执行完毕.总共耗时 {1} ms,删除文件{2},跳过{3}.", DateTime.Now, sw.ElapsedMilliseconds, delNum, ignoreNum);
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileName"></param>
        public static void DelFile(FileInfo file)
        {
            if (Filefilter(file))
            {
                if (config.delFalg)
                {
                    delFiles.Add(file.FullName);
                    if (delFiles.Count() > 1000)
                    {
                        Console.WriteLine("累积需要删除的文件达到了{0}条了,开始并行删除", delFiles.Count());
                        Parallel.ForEach(delFiles, (f) =>
                        {
                            Interlocked.Add(ref delNum, 1);
                            File.Delete(f);
                        });
                    }
                    //File.Delete(file.FullName);
                    delFiles.Clear();
                }
                Console.WriteLine("删除 {0} {1}", file.CreationTime, file.Name);
            }
            else
            {
                Console.WriteLine("跳过 {0} {1} ", file.CreationTime, file.Name);
                ignoreNum++;
            }
            Console.Title = string.Format("删除{0} 跳过{1},运行时长 {2} ms.", delNum, ignoreNum, sw.ElapsedMilliseconds);
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
                           file.CreationTime.AddDays(config.days) < config.now;
        }
    }
}

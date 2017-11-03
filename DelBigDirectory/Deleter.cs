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
        private static long delNum = 0;     //删除文件数
        private static long ignoreNum = 0;    //忽略文件数
        private static Stopwatch sw = new Stopwatch();  //运行时间

        /// <summary>
        /// 初始化服务
        /// 
        /// </summary>
        public static void Init()
        {
            if (Deleter.timer == null)
            {
                TimeSpan dueTime = DateTime.Now.AddSeconds(config.IntervalTime).Subtract(DateTime.Now);
                TimeSpan period = TimeSpan.FromSeconds(config.IntervalTime);
                Deleter.timer = new Timer((state) =>
                {
                    Console.WriteLine("{0} 执行完毕.总共耗时 {1} ms,删除文件{2},跳过{3}.", DateTime.Now, sw.ElapsedMilliseconds, Interlocked.Read(ref delNum), Interlocked.Read(ref ignoreNum));
                    //Deleter.DelAllFile();
                }, null, dueTime, period);
            }
        }

        /// <summary>
        /// 删除所有文件
        /// </summary>
        public static void DelAllFile()
        {
            delNum = 0;
            ignoreNum = 0;
            sw.Restart();
            var dir = new System.IO.DirectoryInfo(config.dir);
            var files = dir.EnumerateFiles("*.*");
            Parallel.ForEach(files, new ParallelOptions() { MaxDegreeOfParallelism = 100 }, (f) =>
            {
                DelFile(f);
            });
            sw.Stop();
            Console.WriteLine("{0} 执行完毕.总共耗时 {1} ms,删除文件{2},跳过{3}.", DateTime.Now, sw.ElapsedMilliseconds, Interlocked.Read(ref delNum), Interlocked.Read(ref ignoreNum));
            Console.ReadKey();
        }


        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileName"></param>
        private static void DelFile(FileInfo file)
        {
            if (Filefilter(file) && config.delFalg)
            {
                Interlocked.Add(ref delNum, 1);
                file.Delete();
            }
            else
            {
                Interlocked.Add(ref ignoreNum, 1);
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
                           file.CreationTime.AddDays(config.days) < config.now;
        }
    }
}

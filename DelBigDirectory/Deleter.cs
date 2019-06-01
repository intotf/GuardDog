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
        private static Timer TitleTimer;
        private static long delNum = 0;     //删除文件数
        private static long ignoreNum = 0;    //忽略文件数
        private static Stopwatch sw = new Stopwatch();  //运行时间

        /// <summary>
        /// 初始化服务
        /// </summary>
        public static void Init()
        {
            if (Deleter.timer == null)
            {
                TimeSpan dueTime = DateTime.Now.AddDays(config.IntervalTime).Subtract(DateTime.Now);
                TimeSpan period = TimeSpan.FromDays(config.IntervalTime);
                Deleter.timer = new Timer((state) =>
                {
                    Deleter.DelAllFile();
                }, null, dueTime, period);
            }
        }

        /// <summary>
        /// 间隔1秒输出执行结果
        /// </summary>
        private static void ConsoleWrite()
        {
            ////// 第一个参数是：回调方法，表示要定时执行的方法，
            ////// 第二个参数是：回调方法要使用的信息的对象，或者为空引用，
            ////// 第三个参数是：调用 callback 之前延迟的时间量（以毫秒为单位），指定 Timeout.Infinite 以防止计时器开始计时。指定零 (0) 以立即启动计时器。第四个参数是：定时的时间时隔，以毫秒为单位
            if (Deleter.TitleTimer == null)
            {
                var dueTilteTime = DateTime.Now.AddSeconds(1).Subtract(DateTime.Now);
                Deleter.TitleTimer = new Timer((state) =>
                {
                    Console.Title = string.Format("当前耗时 {0} ms,删除{1},跳过{2}.", sw.ElapsedMilliseconds, delNum, ignoreNum);
                }, null, dueTilteTime, TimeSpan.FromSeconds(1));
            }
        }

        /// <summary>
        /// 删除所有文件
        /// </summary>
        public static void DelAllFile()
        {

            Dispose();
            var dir = new System.IO.DirectoryInfo(config.dir);
            var files = dir.EnumerateFiles("*.*", SearchOption.AllDirectories);
            foreach (var item in files)
            {
                DelFile(item);
            }
            sw.Stop();
            Deleter.TitleTimer.Dispose();
            Deleter.TitleTimer = null;

            Console.WriteLine("{0} 执行完毕,总共耗时 {1}ms,删除 {2},跳过 {3}.", DateTime.Now, sw.ElapsedMilliseconds, delNum, ignoreNum);
            Console.ReadKey();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private static void Dispose()
        {
            Console.WriteLine("{0} 开始一轮新的执行", DateTime.Now);
            delNum = 0;
            ignoreNum = 0;
            sw.Restart();
            ConsoleWrite();
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileName"></param>
        private static void DelFile(FileInfo file)
        {
            if (Filefilter(file) && config.delFalg)
            {
                delNum++;
                file.Delete();
            }
            else
            {
                ignoreNum++;
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

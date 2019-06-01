using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topshelf;

namespace DelBigDirectory
{
    class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.SetOut(Debugger.Out);
            }
            HostFactory.Run(c =>
            {
                c.Service<ServerControl>();
                c.RunAsLocalSystem();
                c.SetServiceName("FileDeletes");
                c.SetDisplayName("FileDeletes");
                c.SetDescription("大数据文件批量删除小工具");
            });

        }
    }
}

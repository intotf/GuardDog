using Infrastructure.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Topshelf;

namespace GuardDog
{
    static class Program
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
                HostFactory.Run(c =>
                {
                    c.Service<ServerControl>();
                    c.RunAsLocalSystem();
                    c.SetServiceName("GuardDog");
                    c.SetDisplayName("看门狗");
                    c.SetDescription("服务与进程看门狗");
                });
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}

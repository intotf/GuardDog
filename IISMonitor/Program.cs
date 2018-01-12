using Infrastructure.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Topshelf;

namespace IISMonitor
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
                    c.SetServiceName("IISMonitor");
                    c.SetDisplayName("IIS 网站监控");
                    c.SetDescription("IIS 网站监控程序/服务,QQ:42309073");
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

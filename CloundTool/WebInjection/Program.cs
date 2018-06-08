using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebInjection
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetIeVersion();
            Application.Run(new MainForm());
        }

        static void SetIeVersion()
        {
            using (var web = new WebBrowser())
            {
                var emulation = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
                var key = Process.GetCurrentProcess().ProcessName + ".exe";
                var value = web.Version.Major + "000";
                emulation.SetValue(key, value, RegistryValueKind.DWord);
            }
        }
    }
}

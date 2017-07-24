using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace GuardDog
{
    /// <summary>
    /// 应用程序类监控
    /// </summary>
    public class ProcessMonitor : MonitorBase
    {
        /// <summary>
        /// 程序所在路径
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// 程序名称
        /// </summary>
        private readonly string processName;

        public override string TargetName
        {
            get { return this.processName; }
        }

        /// <summary>
        /// 程序监控构造函数
        /// </summary>
        /// <param name="path"></param>
        public ProcessMonitor(string path)
        {
            this.filePath = Path.GetFullPath(path);
            this.processName = Path.GetFileNameWithoutExtension(path);
        }

        /// <summary>
        /// 检查程序是否运行
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> CheckTargetAsync()
        {
            var processes = Process.GetProcessesByName(this.processName);
            var state = processes.Any(item => string.Equals(this.filePath, item.MainModule.FileName, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(state);
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> RunTargetAsync()
        {
            ProcessEx.Start(new ProcessStartInfo(this.filePath));
            return Task.FromResult(true);
        }
    }
}

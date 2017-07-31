using System.Threading;

namespace GuardDog
{
    /// <summary>
    /// 控制接口
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// 开始监控
        /// </summary>
        /// <param name="cancellationToken">取消token</param>
        void StartAsync(CancellationToken cancellationToken);
    }
}


namespace U.Threading
{
    /// <summary>
    /// app self threaded 服务（任务）的定义
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// 开启任务
        /// </summary>
        void Start();

        /// <summary>
        /// 停止任务
        /// 如果想停止异步Async任务，则客户端还需要调用WaitToStop();
        /// </summary>
        void Stop();

        /// <summary>
        /// 等待任务结束后停止
        /// </summary>
        void WaitToStop();
    }
}

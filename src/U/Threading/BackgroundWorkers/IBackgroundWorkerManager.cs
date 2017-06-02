
namespace U.Threading.BackgroundWorkers
{
    /// <summary>
    /// used to manage background workers(thread).
    /// </summary>
    public interface IBackgroundWorkerManager : IRunnable
    {
        /// <summary>
        /// add a new worker, starts the worker immediately if <see cref="IBackgroundWorkerManager"/> has started
        /// 添加一新的worker, 如果它的状态是启动中，则会立即开始工作
        /// </summary>
        /// <param name="worker">the worker, it should be resolved from IOC</param>
        void Add(IBackgroundWorker worker);
    }
}


namespace U.Threading.BackgroundWorkers
{
    /// <summary>
    /// interface for a worker(thread) that runs on background to perform some tasks.
    /// 定义 worker(thread) 接口用于跑在后台上，需要它来执行的一些任务 
    /// </summary>
    public interface IBackgroundWorker : IRunnable
    {
    }
}

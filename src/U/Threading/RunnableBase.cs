
namespace U.Threading
{
    /// <summary>
    /// 基类实现IRunnable
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        private volatile bool _isRunning;
        /// <summary>
        /// 控制当前任务的运行状态
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        public virtual void Start()
        {
            _isRunning = true;
        }

        public virtual void Stop()
        {
            _isRunning = false;
        }

        public virtual void WaitToStop()
        {

        }
    }
}

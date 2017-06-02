using U.Logging;

namespace U.Threading.BackgroundWorkers
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundWorker"/>
    /// </summary>
    public abstract class BackgroundWorkerBase : RunnableBase, IBackgroundWorker
    {
        public ILogger Logger { get; set; }

        public BackgroundWorkerBase()
        {
            Logger = NullLogger.Instance;
        }

        public override void Start()
        {
            base.Start();
            Logger.Debug("Start background worker：" + ToString());
        }

        public override void Stop()
        {
            base.Stop();
            Logger.Debug("Stop background worker：" + ToString());
        }

        public override void WaitToStop()
        {
            base.WaitToStop();
            Logger.Debug("WaitToStop background worker：" + ToString());
        }

        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}

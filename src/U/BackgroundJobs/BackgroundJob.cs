using U.Logging;

namespace U.BackgroundJobs
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundJob{TArgs}"/>
    /// 基类，能被用于实现IBackgroundJob
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        public ILogger Logger { protected get; set; }

        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
        }

        public abstract void Execute(TArgs args);
    }
}

using Quartz;
using U.Logging;

namespace U.Quartz
{
    public abstract class JobBase : IJob
    {
        protected JobBase()
        {
            Logger = NullLogger.Instance;
        }
        public ILogger Logger { protected get; set; }

        public abstract void Execute(IJobExecutionContext context);
    }
}

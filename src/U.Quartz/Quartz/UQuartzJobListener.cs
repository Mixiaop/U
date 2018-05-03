using Quartz;
using U.Logging;

namespace U.Quartz
{
    public class UQuartzJobListener : IJobListener
    {
        public string Name { get; } = "UJobListener";

        public ILogger Logger { get; set; }

        public UQuartzJobListener()
        {
            Logger = NullLogger.Instance;
        }

        public virtual void JobExecutionVetoed(IJobExecutionContext context)
        {
            Logger.Information($"Job {context.JobDetail.JobType.Name} executing operation vetoed...");
        }

        public virtual void JobToBeExecuted(IJobExecutionContext context)
        {
            Logger.Debug($"Job {context.JobDetail.JobType.Name} executing...");
        }

        public virtual void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            if (jobException == null)
            {
                Logger.Debug($"Job {context.JobDetail.JobType.Name} sucessfully executed.");
            }
            else
            {
                Logger.Error($"Job {context.JobDetail.JobType.Name} failed with exception: {jobException}");
            }
        }
    }
}

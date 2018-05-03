using Quartz;

namespace U.Quartz.Startup
{
    public interface IUQuartzConfiguration
    {
        IScheduler Scheduler { get; }
    }
}

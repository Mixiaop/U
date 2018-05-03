using Quartz;
using Quartz.Impl;

namespace U.Quartz.Startup
{
    public class UQuartzConfiguration : IUQuartzConfiguration
    {
        public IScheduler Scheduler => StdSchedulerFactory.GetDefaultScheduler();
    }
}

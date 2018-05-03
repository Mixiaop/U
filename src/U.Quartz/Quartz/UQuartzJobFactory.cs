using Quartz;
using Quartz.Spi;
using U.Dependency;


namespace U.Quartz
{
    public class UQuartzJobFactory : IJobFactory
    {
        private readonly IIocResolver _iocResolver;

        public UQuartzJobFactory(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_iocResolver.Resolve(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            _iocResolver.Release(job);
        }
    }
}

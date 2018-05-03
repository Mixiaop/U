using Quartz;
using System.Reflection;
using U.Dependency;
using U.Quartz.Startup;
using U.Threading.BackgroundWorkers;
using U.UPrimes;

namespace U.Quartz
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class UQuartzUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IUQuartzConfiguration, UQuartzConfiguration>();
            //Engine.IocManager.Resolve<IUQuartzConfiguration>().Scheduler.JobFactory = new UQuartzJobFactory(Engine.IocManager);
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            Engine.IocManager.RegisterIfNot<IJobListener, UQuartzJobListener>();

            //Configuration.Modules.AbpQuartz().Scheduler.ListenerManager.AddJobListener(IocManager.Resolve<IJobListener>());

            if (Engine.Configuration.BackgroundJob.IsJobExecutionEnabled)
            {
                Engine.IocManager.Resolve<IBackgroundWorkerManager>().Add(Engine.IocManager.Resolve<IQuartzScheduleJobManager>());
            }
        }
    }
}

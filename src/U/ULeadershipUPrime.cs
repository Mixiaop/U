using System.Reflection;
using U.Dependency;
using U.Threading;
using U.Threading.BackgroundWorkers;
using U.BackgroundJobs;
using U.UPrimes;
using U.Domain.Uow;
using U.Startup.Configuration;

namespace U
{
    /// <summary>
    /// Core UPrime of the system
    /// it's automatically the first UPrime always.
    /// </summary>
    public sealed class ULeadershipUPrime : UPrime
    {

        public override void PreInitialize()
        {
            Engine.IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //default config path
            Engine.Configuration.Settings.SettingsPath = "/Config/U/";

            var config = Engine.IocManager.Resolve<IUnitOfWorkConfiguration>();
            config.UnitOfWork.RegisterFilter(UDataFilters.SoftDelete, true);
        }

        public override void Initialize()
        {
            base.Initialize();

            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();
            if (Engine.Configuration.BackgroundJob.IsJobExecutionEnabled)
            {
                var workerManager = UPrimeEngine.Instance.Resolve<IBackgroundWorkerManager>();
                workerManager.Start();
                workerManager.Add(UPrimeEngine.Instance.Resolve<IBackgroundJobManager>());
            }

            //inject interceptors for IApplication or IRepository
            //var builder = new ContainerBuilder();
            //builder.RegisterModule(new UnitOfWorkModule());
            //builder.Update(Engine.IocManager.IocContainer);
        }

        public override void Shutdown()
        {
            
            if (Engine.Configuration.BackgroundJob.IsJobExecutionEnabled)
            {
                Engine.Resolve<IBackgroundWorkerManager>().StopAndWaitToStop();
            }
        }

        #region Utilities
        private void RegisterMissingComponents()
        {
            Engine.IocManager.RegisterIfNot<IGuidGenerator, SequentialGuidGenerator>(DependencyLifeStyle.Transient);
            Engine.IocManager.RegisterIfNot<IUnitOfWork, NullUnitOfWork>(DependencyLifeStyle.Transient);
        }
        #endregion
    }
}

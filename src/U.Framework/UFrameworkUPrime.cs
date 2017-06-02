using System.Reflection;
using Autofac;
using U.UPrimes;
using U.Startup.Configuration;
using U.Domain.Uow;
using U.EventBus;

namespace U
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class UFrameworkUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>();
            Engine.IocManager.Register<IUnitOfWorkConfiguration, UnitOfWorkConfiguration>();
            Engine.IocManager.Register<IEventBus, U.EventBus.EventBus>();

            var builder = new ContainerBuilder();
            builder.Update(Engine.IocManager.IocContainer);

            var config = Engine.IocManager.Resolve<IUnitOfWorkConfiguration>();
            config.UnitOfWork.RegisterFilter(UDataFilters.SoftDelete, true);
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //inject interceptors for IApplication or IRepository
            //var builder = new ContainerBuilder();
            //builder.RegisterModule(new UnitOfWorkModule());
            //builder.Update(Engine.IocManager.IocContainer);
        }
    }
}

using System.Reflection;
using Autofac;
using U.UPrimes;
using U.Logging;
using U.Reflection;
using U.EntityFramework.Repositories;
using U.EntityFramework.Uow;
using U.EntityFramework.Startup.Configuration;
using U.EntityFramework.Startup.Dependecy;

namespace U.EntityFramework
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class UEntityFrameworkUPrime : U.UPrimes.UPrime
    {
        private readonly ITypeFinder _typeFinder;
        public UEntityFrameworkUPrime(ITypeFinder typeFinder) {
            _typeFinder = typeFinder;
        }
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IEFConfiguration, EFConfiguration>();
            Engine.IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //var builder = new ContainerBuilder();
            //builder.RegisterGeneric(typeof(UnitOfWorkDbContextProvider<>)).As(typeof(IDbContextProvider<>)).SingleInstance();
            //builder.RegisterGeneric(typeof(SimpleDbContextProvider<>)).As(typeof(IDbContextProvider<>)).SingleInstance();
            //builder.Update(Engine.IocManager.IocContainer);

            RegisterGenericRepositories();
        }

        private void RegisterGenericRepositories()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(UDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
               LogHelper.Logger.Warning("No class found derived from UDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, Engine.IocManager);
            }
        }
    }
}

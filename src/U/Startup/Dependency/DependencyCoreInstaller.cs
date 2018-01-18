using System;
using System.Reflection;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using U.Reflection;
using U.Events.Bus;
using U.UPrimes;
using U.Logging;
using U.Settings;
using U.Dependency;
using U.Domain.Uow;
using U.Runtime.Caching;
using U.Startup.Configuration;

namespace U.Startup
{
    /// <summary>
    /// 依赖注册核心安装器，框架核心的依赖都在这里安装及配置
    /// </summary>
    public class DependencyCoreInstaller
    {
        public IIocManager Install()
        {
            var builder = new ContainerBuilder();
            var typeFinder = new TypeFinder();

            builder.RegisterModule(new LoggingModule());
            builder.RegisterModule(new EventBusModule());
            builder.RegisterModule(new UnitOfWorkModule());
            //builder.RegisterModule(new CachingModule());
            builder.RegisterSource(new SettingsSource());

            builder.RegisterType<UnitOfWorkDefaultOptions>().As<IUnitOfWorkDefaultOptions>().SingleInstance();
            builder.RegisterType<UnitOfWorkConfiguration>().As<IUnitOfWorkConfiguration>().SingleInstance();
            builder.RegisterType<LoggingConfiguration>().As<ILoggingConfiguration>().SingleInstance();
            builder.RegisterType<SettingsConfiguration>().As<ISettingsConfiguration>().SingleInstance();
            builder.RegisterType<CachingConfiguration>().As<ICachingConfiguration>().SingleInstance();
            builder.RegisterType<BackgroundJobConfiguration>().As<IBackgroundJobConfiguration>().SingleInstance();
            builder.RegisterType<UStartupConfiguration>().As<IUStartupConfiguration>().SingleInstance();
            builder.RegisterType<TypeFinder>().As<ITypeFinder>().SingleInstance();
            builder.RegisterType<UPrimeManager>().As<IUPrimeManager>().SingleInstance();

            builder.RegisterType<DefaultUPrimeFinder>().As<IUPrimeFinder>().SingleInstance();
            builder.RegisterType<JSONSettingsManager>().As<ISettingsManager>().SingleInstance();

            builder.RegisterType<CachingInterceptor>().As<CachingInterceptor>();

            var iocManager = new IocManager(builder.Build());
            return iocManager;
        }
    }

    #region USettingsSource
    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(USettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : USettings<TSettings>, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    return c.Resolve<ISettingsManager>().GetSettings<TSettings>();
                })
                .SingleInstance()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
    #endregion
}

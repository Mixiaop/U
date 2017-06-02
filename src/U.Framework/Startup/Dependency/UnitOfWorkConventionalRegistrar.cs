using Autofac;
using U.Dependency;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Domain.Uow;
using U.Application.Services;

namespace U.Startup.Dependency
{
    public class UnitOfWorkConventionalRegistrar : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// registers types of given assembly of convention.
        /// </summary>
        /// <param name="context"></param>
        public void RegisterAssembly(IConventionalRegistrationContext context) {

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(context.Assembly)
                .As<IApplicationService>()
                .InterceptedBy(typeof(UnitOfWorkInterceptor));

            var a = builder.RegisterAssemblyTypes(context.Assembly)
                .As<IApplicationService>();

            builder.Update(context.IocManager.IocContainer);
            
        }
    }
}

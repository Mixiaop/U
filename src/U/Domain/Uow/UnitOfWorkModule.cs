using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Application.Services;
using U.Domain.Repositories;

namespace U.Domain.Uow
{
    public class UnitOfWorkModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWorkInterceptor>();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (DynamicProxyContext.From(registration) != null)
            {
                //var implementationType = registration.Activator.LimitType;
                //if (typeof(IRepository).IsAssignableFrom(implementationType) ||
                //    typeof(IApplicationService).IsAssignableFrom(implementationType))
                //{
                //    registration.InterceptedBy<UnitOfWorkInterceptor>();
                //}
                //else if (implementationType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                //    .Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
                //{
                //    registration.InterceptedBy<UnitOfWorkInterceptor>();
                //}
            }
        }
    }
}

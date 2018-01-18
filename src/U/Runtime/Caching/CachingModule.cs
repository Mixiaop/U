using System;
using System.Reflection;
using System.Linq;
using Autofac;
using Autofac.Core;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Application.Services;

namespace U.Runtime.Caching
{
    public class CachingModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CachingInterceptor>();
        }

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            if (DynamicProxyContext.From(registration) != null)
            {
                var a = 1;
                //var implementationType = registration.Activator.LimitType;

                //if (implementationType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                //    .Any(CachingHelper.HasCachingAttribute))
                //{
                //    registration.InterceptedBy<CachingInterceptor>();
                //}
            }
        }

        
    }
}

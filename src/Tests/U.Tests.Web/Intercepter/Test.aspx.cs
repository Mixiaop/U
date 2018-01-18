using System;
using Autofac;
using Autofac.Core;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using U.Logging;
using U.Dependency.AutofacUtils.DynamicProxy2;

namespace U.Tests.Web.Intercepter
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var context = new DynamicProxyContext();

            //var builder1 = new ContainerBuilder();
            //builder1.RegisterType<SimpleComponent>().As<SimpleComponent>().EnableDynamicProxy(context);
            //builder1.RegisterModule(new SimpleInterceptorModule());
            //var container1 = builder1.Build();

            //var simple1 = container1.Resolve<SimpleComponent>();

            //var builder2 = new ContainerBuilder();
            //builder2.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            //var container2 = builder2.Build();

            //var simple2 = container2.Resolve<SimpleComponent>();

            //Response.Write(simple2.SimpleMethod());
            //Response.Write(simple1.SimpleMethod());
            var simple = UPrimeEngine.Instance.Resolve<SimpleComponent>();
            Response.Write(simple.SimpleMethod());
        }
    }

    public class SimpleComponent : U.Application.Services.IApplicationService
    {
        public virtual string SimpleMethod()
        {
            return "default return value";
        }
    }

    public class SimpleInterceptorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleInterceptor>();

            base.Load(builder);
        }

        protected override void AttachToComponentRegistration(
            IComponentRegistry componentRegistry,
            IComponentRegistration registration)
        {

            if (DynamicProxyContext.From(registration) != null)
                registration.InterceptedBy<SimpleInterceptor>();
        }
    }

    public class SimpleInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "SimpleMethod")
            {
                invocation.ReturnValue = "different return value";
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
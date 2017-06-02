using System;
using Autofac;
using Autofac.Core;
using Castle.DynamicProxy;
using U.Tests.ConsoleApp.AutofacUtils.DynamicProxy2;

namespace U.Tests.ConsoleApp.AutofacUtils
{
    public class AutofacInterceptorTests
    {
        public void Run()
        {
            var dynaimicProxyContext = new DynamicProxyContext();
            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleComponent>().EnableDynamicProxy(dynaimicProxyContext);
            builder.RegisterModule(new SimpleInterceptorModule());
            var container = builder.Build();
            Console.WriteLine("resolve SimpleComponent：");

            var simple = container.Resolve<SimpleComponent>();
            simple.SimpleMethod();

            //var context = new DynamicProxyContext();


            //Type proxyType;
            //bool result = context.TryGetProxy(typeof(SimpleComponent), out proxyType);
            //Console.WriteLine("TryGetProxy1：" + result);

            //context.AddProxy(typeof(SimpleComponent));
            //result = context.TryGetProxy(typeof(SimpleComponent), out proxyType);
            //Console.WriteLine("TryGetProxy2：" + result);
        }
    }

    public class SimpleComponent
    {
        public virtual void SimpleMethod()
        {
            Console.WriteLine("default return value");
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
            Console.WriteLine("intercepted call " + invocation.Method.Name + "different return value");
            invocation.Proceed();

            //if (invocation.Method.Name == "SimpleMethod")
            //{
            //    invocation.ReturnValue = "different return value";
            //}
            //else
            //{
            //    invocation.Proceed();
            //}
        }
    }
}

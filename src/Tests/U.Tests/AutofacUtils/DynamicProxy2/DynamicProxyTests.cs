using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using Autofac.Core;
using Autofac.Features.Metadata;
using Castle.DynamicProxy;
using U.Dependency.AutofacUtils.DynamicProxy2;

namespace U.Tests.AutofacUtils.DynamicProxy2
{
    [TestClass]
    public class DynamicProxyTests
    {
        [TestMethod]
        public void ContextAddedToMetadataWhenRegistered()
        {
            var context = new DynamicProxyContext();

            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleComponent>().EnableDynamicProxy(context);

            var container = builder.Build();

            var meta = container.Resolve<Meta<SimpleComponent>>();
            Assert.AreSame(meta.Metadata["U.Dependency.AutofacUtils.DynamicProxy2.DynamicProxyContext.ProxyContextKey"], context);
        }

        [TestMethod]
        public void ProxyContextReturnsTrueIfTypeHasBeenProxied()

        {
            var context = new DynamicProxyContext();

            Type proxyType;
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), false);
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), false);
            Assert.IsNull(proxyType);

            context.AddProxy(typeof(SimpleComponent));
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), true);
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), true);
            Assert.IsNotNull(proxyType);
       } 

        [TestMethod]
        public void AddProxyCanBeCalledMoreThanOnce()
        {
            var context = new DynamicProxyContext();
            context.AddProxy(typeof(SimpleComponent));

            Type proxyType;
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), true);
            Assert.IsNotNull(proxyType);

            Type proxyType2;
            context.AddProxy(typeof(SimpleComponent));
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType2), true);

            Assert.AreSame(proxyType2, proxyType);
        }

        [TestMethod]
        public void InterceptorAddedToContextFromModules()
        {
            var context = new DynamicProxyContext();

            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            builder.RegisterModule(new SimpleInterceptorModule());

            builder.Build();

            Type proxyType;
            Assert.AreEqual(context.TryGetProxy(typeof(SimpleComponent), out proxyType), true);
            Assert.IsNotNull(proxyType);
        }

        [TestMethod]
        public void ResolvedObjectIsSubclass() {
            var context = new DynamicProxyContext();

            var builder = new ContainerBuilder();
            builder.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            builder.RegisterModule(new SimpleInterceptorModule());

            var container = builder.Build();

            var simpleComponent = container.Resolve<SimpleComponent>();

            Assert.IsInstanceOfType(simpleComponent, typeof(SimpleComponent));
            Assert.AreNotEqual(simpleComponent, typeof(SimpleComponent));
        }

        [TestMethod]
        public void InterceptorCatchesMethodCallOnlyFromContainerWithInterceptor()
        {
            var context = new DynamicProxyContext();

            var builder1 = new ContainerBuilder();
            builder1.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            builder1.RegisterModule(new SimpleInterceptorModule());
            var container1 = builder1.Build();

            var simple1 = container1.Resolve<SimpleComponent>();

            //var builder2 = new ContainerBuilder();
            //builder2.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            //var container2 = builder2.Build();

            //var simple2 = container2.Resolve<SimpleComponent>();

            Console.WriteLine(simple1.SimpleMethod());
            Assert.AreEqual("different return value", simple1.SimpleMethod());
            //Assert.AreEqual(simple2.SimpleMethod(), "default return value");
        }

        [TestMethod]
        public void InterceptorCatchesMethodCallOnlyFromContainerWithInterceptor2() {
            var context = new DynamicProxyContext();

            var builder1 = new ContainerBuilder();
            builder1.RegisterType<SimpleComponent>().EnableDynamicProxy(context);
            var container1 = builder1.Build();

            var builder2 = new ContainerBuilder();
            builder2.RegisterModule(new SimpleInterceptorModule());
            builder2.Update(container1);

            var simple1 = container1.Resolve<SimpleComponent>();

            Console.WriteLine(simple1.SimpleMethod());
            Assert.AreEqual("different return value", simple1.SimpleMethod());
        }
    }

    public class SimpleComponent
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

    [Serializable]
    public class SimpleInterceptor : IInterceptor
    {
        public SimpleInterceptor() { }
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

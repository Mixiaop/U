using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using U;
using U.Dependency;
using U.Domain.Uow;

namespace U.Tests.Domain.Uow
{
    [TestClass]
    public class UnitOfWorkManager_Tests
    {
        [TestMethod]
        public void Should_Call_Uow_Methods()
        {
            

            UStarterHelperTests.Start();
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWorkDefaultOptions>().As<IUnitOfWorkDefaultOptions>().SingleInstance();
            builder.RegisterType<CallContextCurrentUnitOfWorkProvider>().As<ICurrentUnitOfWorkProvider>().SingleInstance();
            builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().SingleInstance();
            builder.RegisterType<NullUnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.Update(UPrimeEngine.Instance.IocManager.IocContainer);
            //builder.RegisterType<SettingsConfiguration>().As<IUnitOfWork>().SingleInstance();
            //    Component.For<IUnitOfWork>().UsingFactoryMethod(() => fakeUow).LifestyleSingleton()

            var uowManager = UPrimeEngine.Instance.Resolve<IUnitOfWorkManager>();

            var fakeUow = UPrimeEngine.Instance.Resolve<IUnitOfWork>();
            //Starting the first uow
            using (var uow1 = uowManager.Begin())
            {
                ////so, begin will be called
                //fakeUow.Begin();

                ////trying to begin a uow (not starting a new one, using the outer)
                //using (var uow2 = uowManager.Begin())
                //{
                //    //Since there is a current uow, begin is not called
                //    fakeUow.Received(1).Begin(Arg.Any<UnitOfWorkOptions>());

                //    uow2.Complete();

                //    //complete has no effect since outer uow should complete it
                //    fakeUow.DidNotReceive().Complete();
                //}

                ////trying to begin a uow (forcing to start a NEW one)
                //using (var uow2 = uowManager.Begin(TransactionScopeOption.RequiresNew))
                //{
                //    //So, begin is called again to create an inner uow
                //    fakeUow.Received(2).Begin(Arg.Any<UnitOfWorkOptions>());

                //    uow2.Complete();

                //    //And the inner uow should be completed
                //    fakeUow.Received(1).Complete();
                //}

                //complete the outer uow
                uow1.Complete();
            }

            //fakeUow.Received(2).Complete();
            //fakeUow.Received(2).Dispose();
        }
    }
}

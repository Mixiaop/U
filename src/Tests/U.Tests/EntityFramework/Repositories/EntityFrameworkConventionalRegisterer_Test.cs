using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U.Dependency;
using U.EntityFramework;
using U.EntityFramework.Startup.Configuration;
using U.EntityFramework.Startup.Dependecy;

namespace U.Tests.EntityFramework.Repositories
{
    [TestClass]
    public class EntityFrameworkConventionalRegisterer_Test : EntityFrameworkTestBase
    {
        [TestMethod]
        public void Should_Set_ConnectionString_If_Configured()
        {
            UPrimeEngine.Instance.Register<IEFConfiguration, MyEFConfiguration>();
            new EntityFrameworkConventionalRegistrar().RegisterAssembly(
                new ConventionalRegistrationContext(
                        Assembly.GetExecutingAssembly(),
                        UPrimeEngine.Instance.IocManager
                        )
                );


            
            //UPrimeEngine.Instance.IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegistrar());

            var context1 = UPrimeEngine.Instance.Resolve<MyDbContext>();
        }

        public class MyEFConfiguration : IEFConfiguration {

            public bool OpenedReadAndWrite
            {
                get { return false; }
            }

            public string SqlConnectionString
            {
                get { return "Server=localhost;Database=test;User=sa;Password=123"; }
            }

            public string ReadSqlConnectionString
            {
                get { return "Server=localhost;Database=test;User=sa;Password=123"; }
            }
        }

        public class MyDbContext : UDbContext
        {
            public bool CalledConstructorWithConnectionString { get; private set; }

            public MyDbContext()
            {

            }

            public MyDbContext(string nameOrConnectionString)
                : base(nameOrConnectionString)
            {
                CalledConstructorWithConnectionString = true;
            }

            public override void Initialize()
            {

            }
        }
    }
}

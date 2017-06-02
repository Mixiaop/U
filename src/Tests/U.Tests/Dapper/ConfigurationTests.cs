using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using U.Dapper.Startup.Configuration;

namespace U.Tests.Dapper
{
    public class TestDapperConfiguration : DapperConfigurationBase
    {
        public TestDapperConfiguration() : base() { }


        public override bool OpenedReadAndWrite
        {
            get { return true; }
        }

        public override string SqlConnectionString
        {
            get { return "Data Source=120.132.57.7,1444;Initial Catalog=Tests;Persist Security Info=True;User ID=sa;Password=youzy.cn"; }
        }

        public override string ReadSqlConnectionString
        {
            get { return ""; }
        }
    }

    [TestClass]
    public class ConfigurationTests {

        [TestMethod]
        public void CreateConfig() {
            UStarterHelperTests.Start();
            UPrimeEngine.Instance.Register<IDapperConfiguration, TestDapperConfiguration>();

            DateTime start = DateTime.Now;
            for (int i = 0; i < 10000; i++)
            {
                var config = UPrimeEngine.Instance.Resolve<IDapperConfiguration>();
            }

            Console.WriteLine((DateTime.Now - start).Milliseconds);
        }
    }
}

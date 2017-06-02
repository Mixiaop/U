using System.Reflection;
using U.UPrimes;
using U.Dapper.Startup.Configuration;
using U.Dapper;

namespace U.Dapper
{
    [DependsOn(
        typeof(ULeadershipUPrime)
        )]
    public class UDapperUPrime : UPrime
    {
        public override void PreInitialize()
        {
            Engine.IocManager.Register<IDapperConfiguration, DapperConfiguration>();
            Engine.IocManager.Register<IDapperContextProvider, DapperContextProvider>();
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

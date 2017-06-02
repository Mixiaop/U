using System.Reflection;
using U.UPrimes;

namespace U.SDK
{
    [DependsOn(typeof(ULeadershipUPrime))]
    public class USDKUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}


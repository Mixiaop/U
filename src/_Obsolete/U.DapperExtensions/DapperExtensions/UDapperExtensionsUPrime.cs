using System.Reflection;

namespace U.DapperExtensions
{
    public class UDapperExtensionsUPrime : U.UPrimes.UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

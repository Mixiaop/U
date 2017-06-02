using System.Reflection;
using U.UPrimes;

namespace U.Web
{

    public class UWebUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

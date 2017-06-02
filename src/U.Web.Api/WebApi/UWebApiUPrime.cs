using System.Reflection;
using U.Web;
using U.UPrimes;

namespace U.WebApi
{
    [DependsOn(typeof(UWebUPrime))]
    public class UWebApiUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

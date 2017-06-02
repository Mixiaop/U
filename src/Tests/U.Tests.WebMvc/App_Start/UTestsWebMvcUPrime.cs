using System.Reflection;
using U.UPrimes;
using U.Web.Mvc;

namespace U.Tests.WebMvc.App_Start
{
    [DependsOn(typeof(UWebMvcUPrime))]
    public class UTestsWebMvcUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
using System.Reflection;
using U.UPrimes;

namespace U.Converters
{
    [DependsOn(
        typeof(ULeadershipUPrime)
        )]
    public class UAsposeWordsUPrime : U.UPrimes.UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

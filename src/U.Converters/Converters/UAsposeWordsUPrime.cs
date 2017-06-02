using System.Reflection;
using U.UPrimes;

namespace U.Converters
{
    [DependsOn(
        typeof(ULeadershipUPrime)
        )]
    public class UAsposeWordsUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

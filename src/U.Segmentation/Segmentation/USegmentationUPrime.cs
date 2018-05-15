using System.Reflection;
using U.UPrimes;

namespace U.Segmentation
{
    public class USegmentationUPrime : U.UPrimes.UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

using System.Reflection;
using U.UPrimes;

namespace U.Segmentation
{
    public class USegmentationUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

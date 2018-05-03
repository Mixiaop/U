using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using U.UPrimes;
using U.Quartz;

namespace U.Tests
{
    [DependsOn(typeof(UQuartzUPrime))]
    public class UTestsUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

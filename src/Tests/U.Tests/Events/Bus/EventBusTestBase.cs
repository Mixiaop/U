using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using U.Events.Bus;

namespace U.Tests.Events.Bus
{
    public abstract class EventBusTestBase
    {
        protected IEventBus EventBus;
        protected EventBusTestBase()
        {
            UStarterHelperTests.Start();
            EventBus = UPrimeEngine.Instance.Resolve<IEventBus>();
            var a = 1;
        }
    }
}

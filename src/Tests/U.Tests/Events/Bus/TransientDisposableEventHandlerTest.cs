using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace U.Tests.Events.Bus
{
    [TestClass]
    public class TransientDisposableEventHandlerTest : EventBusTestBase
    {
        [TestMethod]
        public void Should_Call_Handler_AndDispose()
        {
            EventBus.Register<MySimpleEventData, MySimpleTransientEventHandler>();

            EventBus.Trigger(new MySimpleEventData(1));
            EventBus.Trigger(new MySimpleEventData(2));
            EventBus.Trigger(new MySimpleEventData(3));

            Assert.AreEqual(MySimpleTransientEventHandler.HandleCount, 3);
            Assert.AreEqual(MySimpleTransientEventHandler.DisposeCount, 3);
        }
    }
}

using U.Events.Bus;

namespace U.Tests.Events.Bus
{
    public class MySimpleEventData : EventData
    {
        public int Value { get; set; }

        public MySimpleEventData(int value) {
            Value = value;
        }
    }
}

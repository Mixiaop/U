using System;
using U.Timing;

namespace U.Events.Bus
{
    /// <summary>
    /// 实现 IEeventData，提供来自 event data 的类的基类
    /// </summary>
    [Serializable]
    public abstract class EventData : IEventData
    {
        public DateTime EventTime { get; set; }

        public object EventSource { get; set; }

        protected EventData() {
            EventTime = Clock.Now;
        }
    }
}

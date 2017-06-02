using System;

namespace U.Events.Bus
{
    /// <summary>
    /// 定义event data的接口类，所有event data 都应用实现此接口
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// 事件触发的时间
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// 触发事件的对象
        /// </summary>
        object EventSource { get; set; }
    }
}

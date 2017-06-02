
namespace U.Events.Bus.Handlers
{
    /// <summary>
    /// 定义事件处理接口类，专用处理EventData类型
    /// </summary>
    /// <typeparam name="TIEventData"></typeparam>
    public interface IEventHandler<in TEventData> : IEventHandler
    {
        /// <summary>
        /// 实际的event handle实现此方法
        /// </summary>
        /// <param name="eventData"></param>
        void HandleEvent(TEventData eventData);
    }
}

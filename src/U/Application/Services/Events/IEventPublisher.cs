
namespace U.Application.Services.Events
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    public interface IEventPublisher : IApplicationService
    {
        /// <summary>
        /// 发布事件（通知订阅者）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventMessage"></param>
        void Publish<T>(T eventMessage);
    }
}

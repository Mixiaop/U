
namespace U.Application.Services.Events
{
    /// <summary>
    /// 代表某个事件的订阅者
    /// 目前主要用的场景为【缓存实体】【实体】的更新
    /// </summary>
    /// <typeparam name="T">订阅的事件类型</typeparam>
    public interface IConsumer<T> : U.Dependency.ITransientDependency
    {
        /// <summary>
        /// 接收（订阅的）事件消息并处理
        /// </summary>
        /// <param name="eventMessage"></param>
        void HandleEvent(T eventMessage);
    }
}

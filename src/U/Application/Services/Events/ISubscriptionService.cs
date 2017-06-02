using System.Collections.Generic;

namespace U.Application.Services.Events
{
    /// <summary>
    /// 事件订阅服务，触发事件发告诉所有订阅者
    /// </summary>
    public interface ISubscriptionService : IApplicationService
    {
        /// <summary>
        /// 获取所有指定类型的订阅者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<IConsumer<T>> GetSubscriptions<T>();        
    }
}

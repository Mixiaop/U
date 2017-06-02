using System;
using System.Threading;
using System.Threading.Tasks;

namespace U.Messaging
{
    /// <summary>
    /// 消息发布者
    /// </summary>
    public interface IMessagePublisher
    {
        Task PublishAsync(Type messageType, object message, TimeSpan? delay, CancellationToken cancellationToken = default(CancellationToken));
    }

    public static class MessagePublisherExtensions {
        public static Task PublishAsync<T>(this IMessagePublisher publisher, T message, TimeSpan? delay = null) where T : class {
            return publisher.PublishAsync(typeof(T), message, delay);
        }
    }
}

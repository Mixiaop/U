using System;
using U.Dependency;
using U.Events.Bus.Handlers;

namespace U.Events.Bus.Factories
{
    /// <summary>
    /// handlers using Ioc
    /// </summary>
    public class IocHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// Type of the handler
        /// </summary>
        public Type HandlerType { get; private set; }

        private readonly IIocResolver _iocResolver;

        public IocHandlerFactory(IIocResolver iocResolver, Type handlerType) {
            _iocResolver = iocResolver;
            HandlerType = handlerType;
        }

        /// <summary>
        /// 通过Ioc容器解析对象
        /// </summary>
        /// <returns></returns>
        public IEventHandler GetHandler()
        {
            return (IEventHandler)_iocResolver.Resolve(HandlerType);
        }

        /// <summary>
        /// 使用Ioc容器释放对象
        /// </summary>
        /// <param name="handler"></param>
        public void ReleaseHandler(IEventHandler handler)
        {
            _iocResolver.Release(handler);
        }
    }
}

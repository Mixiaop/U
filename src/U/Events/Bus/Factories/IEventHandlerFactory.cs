using U.Events.Bus.Handlers;

namespace U.Events.Bus.Factories
{
    /// <summary>
    /// 定义一个工厂接口，他的责任是对 event handle 对象的 create/get/release
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// Gets an event handler
        /// </summary>
        /// <returns>The event handler</returns>
        IEventHandler GetHandler();

        /// <summary>
        /// Releases an event handler
        /// </summary>
        /// <param name="handler">handle to released</param>
        void ReleaseHandler(IEventHandler handler);
    }
}

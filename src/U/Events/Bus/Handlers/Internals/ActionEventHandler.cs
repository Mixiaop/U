using System;
using U.Dependency;

namespace U.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// 内部类，起到适配器的作用，将一个Action适配成一个事件处理器EventHandler
    /// </summary>
    /// <typeparam name="TEventData"></typeparam>
    public class ActionEventHandler<TEventData>:  IEventHandler<TEventData>, ITransientDependency
    {
        /// <summary>
        /// Action to handle the event
        /// </summary>
        public Action<TEventData> Action { get; private set; }

        public ActionEventHandler(Action<TEventData> handler) {
            Action = handler;
        }

        /// <summary>
        /// Handlers the event
        /// </summary>
        /// <param name="eventData"></param>
        public void HandleEvent(TEventData eventData)
        {
            Action(eventData);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using U.Logging;
using U.Events.Bus.Factories;
using U.Events.Bus.Factories.Internals;
using U.Events.Bus.Handlers;
using U.Events.Bus.Handlers.Internals;

namespace U.Events.Bus
{
    /// <summary>
    /// 实现 IEventBus 的单例模式
    /// Implements IEventBus as singleton pattern
    /// </summary>
    public class EventBus : IEventBus
    {
        #region Properties
        public static EventBus Default { get { return DefaultInstance; } }
        private static readonly EventBus DefaultInstance = new EventBus();

        public ILogger Logger { get; set; }

        /// <summary>
        /// All registered handler factories.
        /// Key: Type of the event
        /// Value: List of handler factories
        /// </summary>
        private readonly Dictionary<Type, List<IEventHandlerFactory>> _handlerFactories;
        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new <see cref="EventBus"/> instance.
        /// Instead of creating a new instace, you can use <see cref="Default"/> to use Global <see cref="EventBus"/>.
        /// </summary>
        public EventBus()
        {
            _handlerFactories = new Dictionary<Type, List<IEventHandlerFactory>>();
            Logger = NullLogger.Instance;
        }

        #endregion

        #region Public methods

        #region Register
        public IDisposable Register<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            return Register(typeof(TEventData), new ActionEventHandler<TEventData>(action));
        }

        public IDisposable Register<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handler);
        }

        public IDisposable Register<TEventData, THandler>()
            where TEventData : IEventData
            where THandler : IEventHandler<TEventData>, new()
        {
            return Register(typeof(TEventData), new TransientEventHandlerFactory<THandler>());
        }

        public IDisposable Register(Type eventType, IEventHandler handler)
        {
            return Register(eventType, new SingleInstanceHandlerFactory(handler));
        }

        public IDisposable Register<TEventData>(IEventHandlerFactory handlerFactory) where TEventData : IEventData
        {
            return Register(typeof(TEventData), handlerFactory);
        }

        public IDisposable Register(Type eventType, IEventHandlerFactory handlerFactory)
        {
            lock (_handlerFactories)
            {
                GetOrCreateHandlerFactories(eventType).Add(handlerFactory);
                return new FactoryUnregistrar(this, eventType, handlerFactory);
            }
        }
        #endregion

        #region Unregister
        public void Unregister<TEventData>(Action<TEventData> action) where TEventData : IEventData
        {
            lock (_handlerFactories)
            {
                GetOrCreateHandlerFactories(typeof(TEventData))
                    .RemoveAll(
                        factory =>
                        {
                            if (factory is SingleInstanceHandlerFactory)
                            {
                                var singleInstanceFactoru = factory as SingleInstanceHandlerFactory;
                                if (singleInstanceFactoru.HandlerInstance is ActionEventHandler<TEventData>)
                                {
                                    var actionHandler = singleInstanceFactoru.HandlerInstance as ActionEventHandler<TEventData>;
                                    if (actionHandler.Action == action)
                                    {
                                        return true;
                                    }
                                }
                            }

                            return false;
                        });
            }
        }

        public void Unregister<TEventData>(IEventHandler<TEventData> handler) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), handler);
        }

        public void Unregister(Type eventType, IEventHandler handler)
        {
            lock (_handlerFactories)
            {
                GetOrCreateHandlerFactories(eventType)
                    .RemoveAll(
                        factory =>
                        factory is SingleInstanceHandlerFactory && (factory as SingleInstanceHandlerFactory).HandlerInstance == handler
                    );
            }
        }

        public void Unregister<TEventData>(IEventHandlerFactory factory) where TEventData : IEventData
        {
            Unregister(typeof(TEventData), factory);
        }

        public void Unregister(Type eventType, IEventHandlerFactory factory)
        {
            lock (_handlerFactories)
            {
                GetOrCreateHandlerFactories(eventType).Remove(factory);
            }
        }

        public void UnregisterAll<TEventData>() where TEventData : IEventData
        {
            UnregisterAll(typeof(TEventData));
        }

        public void UnregisterAll(Type eventType)
        {
            lock (_handlerFactories)
            {
                GetOrCreateHandlerFactories(eventType).Clear();
            }
        }
        #endregion

        #region Trigger
        public void Trigger<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            Trigger((object)null, eventData);
        }

        public void Trigger<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            Trigger(typeof(TEventData), eventSource, eventData);
        }

        public void Trigger(Type eventType, IEventData eventData)
        {
            Trigger(eventType, null, eventData);
        }

        public void Trigger(Type eventType, object eventSource, IEventData eventData)
        {
            //TODO: This method can be optimized by adding all possibilities to a dictionary.

            eventData.EventSource = eventSource;

            foreach (var factoryToTrigger in GetHandlerFactories(eventType))
            {
                var eventHandler = factoryToTrigger.GetHandler();
                if (eventHandler == null)
                {
                    throw new Exception("Registered event handler for event type " + eventType.Name + " does not implement IEventHandler<" + eventType.Name + "> interface!");
                }

                var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

                try
                {
                    handlerType
                        .GetMethod("HandleEvent", BindingFlags.Public | BindingFlags.Instance, null, new[] { eventType }, null)
                        .Invoke(eventHandler, new object[] { eventData });
                }
                finally
                {
                    factoryToTrigger.ReleaseHandler(eventHandler);
                }
            }

            //Implements generic argument inheritance. See IEventDataWithInheritableGenericArgument
            if (eventType.IsGenericType &&
                eventType.GetGenericArguments().Length == 1 &&
                typeof(IEventDataWithInheritableGenericArgument).IsAssignableFrom(eventType))
            {
                var genericArg = eventType.GetGenericArguments()[0];
                var baseArg = genericArg.BaseType;
                if (baseArg != null)
                {
                    var baseEventType = eventType.GetGenericTypeDefinition().MakeGenericType(genericArg.BaseType);
                    var constructorArgs = ((IEventDataWithInheritableGenericArgument)eventData).GetConstructorArgs();
                    var baseEventData = (IEventData)Activator.CreateInstance(baseEventType, constructorArgs);
                    baseEventData.EventTime = eventData.EventTime;
                    Trigger(baseEventType, eventData.EventSource, baseEventData);
                }
            }
        }


        #region private
        private IEnumerable<IEventHandlerFactory> GetHandlerFactories(Type eventType)
        {
            var handlerFactoryList = new List<IEventHandlerFactory>();

            lock (_handlerFactories)
            {
                foreach (var handlerFactory in _handlerFactories.Where(hf => ShouldTriggerEventForHandler(eventType, hf.Key)))
                {
                    handlerFactoryList.AddRange(handlerFactory.Value);
                }
            }

            return handlerFactoryList.ToArray();
        }

        private static bool ShouldTriggerEventForHandler(Type eventType, Type handlerType)
        {
            //Should trigger same type
            if (handlerType == eventType)
            {
                return true;
            }

            //Should trigger 
            if (handlerType.IsAssignableFrom(eventType))
            {
                return true;
            }

            return false;
        }
        #endregion

        public Task TriggerAsync<TEventData>(TEventData eventData) where TEventData : IEventData
        {
            return TriggerAsync((object)null, eventData);
        }

        public Task TriggerAsync<TEventData>(object eventSource, TEventData eventData) where TEventData : IEventData
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }

        public Task TriggerAsync(Type eventType, IEventData eventData)
        {
            return TriggerAsync(eventType, null, eventData);
        }

        public Task TriggerAsync(Type eventType, object eventSource, IEventData eventData)
        {
            ExecutionContext.SuppressFlow();

            var task = Task.Factory.StartNew(
                () =>
                {
                    try
                    {
                        Trigger(eventType, eventSource, eventData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning(ex.ToString(), ex);
                    }
                });

            ExecutionContext.RestoreFlow();

            return task;
        }
        #endregion

        #endregion

        #region Private methods

        private List<IEventHandlerFactory> GetOrCreateHandlerFactories(Type eventType)
        {
            List<IEventHandlerFactory> handlers;
            if (!_handlerFactories.TryGetValue(eventType, out handlers))
            {
                _handlerFactories[eventType] = handlers = new List<IEventHandlerFactory>();
            }

            return handlers;
        }

        #endregion
    }
}

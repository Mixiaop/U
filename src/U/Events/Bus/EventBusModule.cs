using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Autofac.Builder;
using Castle.DynamicProxy;
using Module = Autofac.Module;
using U.Events.Bus.Handlers;

namespace U.Events.Bus
{
    public class EventBusModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterInstance(EventBus.Default).As<IEventBus>();
            moduleBuilder.RegisterSource(new EventBusRegistrationSource());
        }
    }

    public class EventBusRegistrationSource : IRegistrationSource { 
    private readonly DefaultProxyBuilder _proxyBuilder;

    public EventBusRegistrationSource()
    {
            _proxyBuilder = new DefaultProxyBuilder();
        }

        public bool IsAdapterForIndividualComponents {
            get { return false; }
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor) {
            var serviceWithType = service as IServiceWithType;
            if (serviceWithType == null)
                yield break;

            var serviceType = serviceWithType.ServiceType;
            if (!serviceType.IsInterface || !typeof(IEventHandler).IsAssignableFrom(serviceType) || serviceType == typeof(IEventHandler))
                yield break;

            var interfaceProxyType = _proxyBuilder.CreateInterfaceProxyTypeWithoutTarget(
                serviceType,
                new Type[0],
                ProxyGenerationOptions.Default);


            var rb = RegistrationBuilder
                .ForDelegate((ctx, parameters) => {
                    var interceptors = new IInterceptor[] {};
                    //var interceptors = new IInterceptor[] { new EventsInterceptor(ctx.Resolve<IEventBus>()) };
                    var args = new object[] { interceptors, null };
                    return Activator.CreateInstance(interfaceProxyType, args);
                })
                .As(service);

            yield return rb.CreateRegistration();
        }
    }
}

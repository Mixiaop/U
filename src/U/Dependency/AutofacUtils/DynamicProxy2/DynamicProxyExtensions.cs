using System;
using System.Linq;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;

namespace U.Dependency.AutofacUtils.DynamicProxy2
{
    public static class DynamicProxyExtensions
    {

        public static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> EnableDynamicProxy<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> rb,
            DynamicProxyContext dynamicProxyContext)
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {

            dynamicProxyContext.EnableDynamicProxy(rb);

            return rb;
        }

        public static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> EnableDynamicProxy<TLimit, TRegistrationStyle>(
           this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> rb,
            DynamicProxyContext dynamicProxyContext)
        {

            rb.ActivatorData.ConfigurationActions.Add((t, rb2) => rb2.EnableDynamicProxy(dynamicProxyContext));
            return rb;
        }

        public static void InterceptedBy<TService>(this IComponentRegistration cr)
        {
            var dynamicProxyContext = DynamicProxyContext.From(cr);
            if (dynamicProxyContext == null)
                throw new ApplicationException(string.Format("Component {0} was not registered with EnableDynamicProxy", cr.Activator.LimitType));

            dynamicProxyContext.AddInterceptorService(cr, new TypedService(typeof(TService)));
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServices">The interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle>
        InterceptedBy<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
        params Service[] interceptorServices)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            if (interceptorServices == null || interceptorServices.Any(s => s == null))
                throw new ArgumentNullException("interceptorServices");

            object existing;
            if (builder.RegistrationData.Metadata.TryGetValue(DynamicProxyContext.InterceptorServicesKey, out existing))
                builder.RegistrationData.Metadata[DynamicProxyContext.InterceptorServicesKey] =
                ((IEnumerable<Service>)existing).Concat(interceptorServices).Distinct();
            else
                builder.RegistrationData.Metadata.Add(DynamicProxyContext.InterceptorServicesKey, interceptorServices);

            return builder;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceNames">The names of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle>
        InterceptedBy<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
        params string[] interceptorServiceNames)
        {
            if (interceptorServiceNames == null || interceptorServiceNames.Any(n => n == null))
                throw new ArgumentNullException("interceptorServiceNames");

            return InterceptedBy(builder, interceptorServiceNames.Select(n => new KeyedService(n, typeof(IInterceptor))).ToArray());
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceTypes">The types of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle>
        InterceptedBy<TLimit, TActivatorData, TStyle>(
        this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
        params Type[] interceptorServiceTypes)
        {
            if (interceptorServiceTypes == null || interceptorServiceTypes.Any(t => t == null))
                throw new ArgumentNullException("interceptorServiceTypes");

            return InterceptedBy(builder, interceptorServiceTypes.Select(t => new TypedService(t)).ToArray());
        }
    }
}

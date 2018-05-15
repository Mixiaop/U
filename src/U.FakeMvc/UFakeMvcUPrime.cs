using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using U.UPrimes;
using U.Reflection;
using U.FakeMvc.Routes;
using U.FakeMvc.Startup;

namespace U.FakeMvc
{
    public class UFakeMvcUPrime : U.UPrimes.UPrime
    {
        public override void PreInitialize()
        {
            //路由配置
            ITypeFinder typeFinder = Engine.Resolve<ITypeFinder>();
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;

                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.ToList();
            routeProviders.ForEach(x => x.RegisterRoutes(RouteContext.Instance));
            routeProviders.ForEach(x => x.RegisterRewriterRole(RouteContext.Instance));

            Engine.IocManager.Register<IUFakeMvcConfiguration, DefaultUFakeMvcConfiguration>();
        }

        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            
        }
    }
}

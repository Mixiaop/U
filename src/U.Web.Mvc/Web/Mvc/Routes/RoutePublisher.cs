using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using U.Dependency;
using U.Reflection;

namespace U.Web.Mvc.Routes
{
    public class RoutePublisher : IRoutePublisher, ITransientDependency
    {
        protected readonly ITypeFinder typeFinder;
        public RoutePublisher(ITypeFinder typeFinder) {
            this.typeFinder = typeFinder;
        }

        public virtual void RegisterRoutes(RouteCollection routes) {
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes) {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }

            routeProviders = routeProviders.OrderBy(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
    }
}

using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;
using U.UPrimes;
using U.Web.Mvc.Routes;

namespace U.Web.Mvc
{
    [DependsOn(typeof(UWebUPrime))]
    public class UWebMvcUPrime : UPrime
    {
        public override void Initialize()
        {
            Engine.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //register custom routes 
            var routePublisher = Engine.IocManager.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);
        }
    }
}

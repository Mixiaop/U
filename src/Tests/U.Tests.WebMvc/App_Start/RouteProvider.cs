using System.Web.Routing;
using System.Web.Mvc;
using U.Web.Mvc.Routes;

namespace U.Tests.WebMvc.App_Start
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
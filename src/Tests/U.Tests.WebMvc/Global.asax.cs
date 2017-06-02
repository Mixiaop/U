using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using U.Web;

namespace U.Tests.WebMvc
{
    public class MvcApplication : UWebApplication
    {
        protected override void Application_Start(object sender, System.EventArgs e)
        {
            base.Application_Start(sender, e);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}



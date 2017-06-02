using System;
using System.Net;
using U.Settings;
using U.Utilities.Web;
using U.Tests.Web.Infrastructure;
namespace U.Tests.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            new UStarter().Startup();
           
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //U.BaseService.Profiler.ProfilerManager.Start();
            var a = UPrimeEngine.Instance.Resolve<TestSettings>();
        }

        

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            var a = UPrimeEngine.Instance.Resolve<TestSettings>();
            //U.BaseService.Profiler.ProfilerManager.Stop();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }


        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
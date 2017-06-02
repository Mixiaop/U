using System;
using System.Web;
using U.Dependency;
using U.Reflection;

namespace U.Web
{
    public abstract class UWebApplication : HttpApplication
    {
        protected UStarter Starter { get; set; }

        protected UWebApplication() {
            Starter = new UStarter();
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            Starter.Startup();
            Starter.UPrime.IocManager.RegisterIfNot<IAssemblyFinder, WebAssemblyFinder>();
        }

        /// <summary>
        /// This method is called by ASP.NET system on web application shutdown.
        /// </summary>
        protected virtual void Application_End(object sender, EventArgs e)
        {
            Starter.Dispose();
        }

        /// <summary>
        /// This method is called by ASP.NET system when a session starts.
        /// </summary>
        protected virtual void Session_Start(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a session ends.
        /// </summary>
        protected virtual void Session_End(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is called by ASP.NET system when a request starts.
        /// </summary>
        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// This method is called by ASP.NET system when a request ends.
        /// </summary>
        protected virtual void Application_EndRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {

        }
    }
}

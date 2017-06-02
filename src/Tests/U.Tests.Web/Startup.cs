using Microsoft.Owin;
using Owin;
using Hangfire;

[assembly: OwinStartup(typeof(U.Tests.Web.Startup))]

namespace U.Tests.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            //GlobalConfiguration.Configuration.UseSqlServerStorage("Data Source=120.132.57.7,1444;Initial Catalog=Youzy.HangFire;Persist Security Info=True;User ID=sa;Password=youzy.cn");
            //app.UseHangfireServer();
            app.UseHangfireDashboard("/jobs");

        }
    }
}

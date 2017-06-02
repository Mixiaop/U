using U.FakeMvc.Routes;

namespace U.Tests.Web.FakeMvc
{
    public class RouteProviderTests : IRouteProvider
    {
        public void RegisterRoutes(RouteContext context)
        {
            context.AddRoute(new Route()
            {
                Key = "Index",
                Value = "/search?wd={0}&p={1}"
            });
        }

        public void RegisterRewriterRole(RouteContext context)
        {
            context.AddRewriterRule(new RewriterRule()
            {
                LookFor = "/haha",
                SendTo = "/Search.aspx"
            });

            context.AddRewriterRule(new RewriterRule()
            {
                LookFor = "/search",
                SendTo = "/Search.aspx"
            });
        }
    }
}

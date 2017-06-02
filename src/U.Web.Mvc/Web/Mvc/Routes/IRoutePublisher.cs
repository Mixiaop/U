using System.Web.Routing;

namespace U.Web.Mvc.Routes
{
    /// <summary>
    /// 路由的发布者，发布实现IRouteProvider的路由注册
    /// </summary>
    public interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes"></param>
        void RegisterRoutes(RouteCollection routes);
    }
}

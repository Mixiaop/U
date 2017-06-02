using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace U.FakeMvc.Routes
{
    /// <summary>
    /// 路由提供者
    /// </summary>
    public interface IRouteProvider 
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <returns></returns>
        void RegisterRoutes(RouteContext context);

        /// <summary>
        /// 注册URL重置规则
        /// </summary>
        /// <param name="context"></param>
        void RegisterRewriterRole(RouteContext context);
    }
}

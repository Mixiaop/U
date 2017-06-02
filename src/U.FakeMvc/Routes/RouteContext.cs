using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace U.FakeMvc.Routes
{
    /// <summary>
    /// 路由上下文
    /// </summary>
    public class RouteContext
    {
        private List<Route> _routes = new List<Route>();
        private List<RewriterRule> _rewriterRules = new List<RewriterRule>();
        private readonly static RouteContext _instance = new RouteContext();

        public static RouteContext Instance
        {
            get
            {
                return _instance;
            }
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route);
        }

        public void AddRewriterRule(RewriterRule rule)
        {
            _rewriterRules.Add(rule);
        }

        public string GetRouteUrl(string routeName, params object[] paramValues)
        {
            string url = "";
            var route = _routes.Where(x => x.Key.ToLower() == routeName.ToLower()).FirstOrDefault();
            if (route == null)
                throw new Exception("通过 [" + routeName + "] 未找到路由，请检查是否正确");

            try
            {
                url = route.Value;
                Regex reg = new Regex(@"{[0-9]*}", RegexOptions.IgnorePatternWhitespace);
                MatchCollection mateches = reg.Matches(route.Value);
                int count = mateches.Count;
                for (int i = 0; i < count; i++) {
                    if (paramValues.Length > i)
                    {
                        url = url.Replace("{" + i + "}", paramValues[i].ToString());
                    }
                    else {
                        url = url.Replace("{" + i + "}", "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " 【" + routeName + "】");
            }


            return url;
        }

        public Route GetRoute(string routeName)
        {
            var route = _routes.Where(x => x.Key == routeName).FirstOrDefault();
            if (route == null)
                throw new Exception("通过 [" + routeName + "] 未找到路由，请检查是否正确");

            return route;
        }

        public List<RewriterRule> GetRewriterRules() {
            return _rewriterRules;
        }
    }
}

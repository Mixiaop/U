using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using U.FakeMvc.Routes;

namespace U.FakeMvc
{
    public class UFakeMvcHttpModule : System.Web.IHttpModule
    {
        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
            context.Error += new EventHandler(Application_OnError);
        }

        public void Application_OnError(Object sender, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)sender;
            //HttpContext context = application.Context;
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public virtual void Dispose()
        {
            //eventTimer = null;
        }

        /// <summary>
        /// 重写Url
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            // 获得配置规则
            List<RewriterRule> rules = RouteContext.Instance.GetRewriterRules();

            ReriterUrl(rules, context);
        }

        private void ReriterUrl(List<RewriterRule> rules, HttpContext context)
        {
            // 遍历每个规则...
            foreach (var rule in rules)
            {
                // 获得要查找的模式，并且
                // 解析 Url（转换为相应的目录）
                string lookFor = "^" + RewriterUtils.ResolveUrl(context.Request.ApplicationPath,
                  rule.LookFor) + "$";

                // 创建 regex（请注意，已设置 IgnoreCase...）
                Regex re = new Regex(lookFor, RegexOptions.IgnoreCase);
                // 查看是否找到了匹配的规则
                if (re.IsMatch(context.Request.Path))
                {
                    // 找到了匹配的规则 -- 进行必要的替换
                    string sendToUrl = RewriterUtils.ResolveUrl(context.Request.ApplicationPath,
                       re.Replace(context.Request.Path, rule.SendTo));

                    // 重写 URL
                    RewriterUtils.RewriteUrl(context, sendToUrl);
                    break;      // 退出 For 循环
                }
            }
        }
    }
}

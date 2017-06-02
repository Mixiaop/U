using System;
using U.Runtime.Caching;

namespace U.Tests.Web.Pages
{
    public partial class Cache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ICacheManager cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
                var cache = cacheManager.GetCache();
                Response.Write(cache.GetOrDefault("byouzykey"));
                cache.Set("sayhi", "hello world2aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                cache.Set("byouzy", "hello world2aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                var startDate = DateTime.Now;
                int count = 1000;
                Response.Write("StartDate：" + startDate + "<br />");
                Response.Write("GetSettings：" + count);
                for (int i = 0; i < count; i++)
                {
                     Response.Write(cache.GetOrDefault("sayhi"));
                }
                Response.Write("EndDate：" + DateTime.Now + "<br />");
                Response.Write("TotalMillsecond：" + (DateTime.Now - startDate).Milliseconds + "<br />");
            }

        }

        public class BusiManager {
            public static string GetDatas()
            { 
                ICacheManager cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
                var cache = cacheManager.GetCache();
                return cache.GetOrDefault("sayhi") as string;
            }
        }
    }
}
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StackExchange.Redis;
using U.Dependency;
using U.Runtime.Caching;
using U.Runtime.Caching.Redis;

namespace U.Tests.Web.Caches
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //IDistributedCacheManager manager = IocManager.Instance.Resolve<IDistributedCacheManager>();
            //var a = 1;
            //RedisCacheManager cacheService = new RedisCacheManager();



            //ICacheService cacheService2 = new RedisCacheService();


            //var _redisClient = new RedisClient("120.132.94.138", 6379);

            //_redisClient.Set("Tests.hello", "heybenZ");
            //using (var client = ((RedisCacheService)cacheService).GetClient()) {
            //    //client.Set("Tests.hello", "haha123");
            //    //Response.Write(client.Get<string>("Tests.hello"));
            //}
            //Response.Write("hello");

            //设置
            //var startTime = DateTime.Now;
            //Response.Write("----SetValue()<br />");
            //Response.Write("----StartTime：" + startTime + "<br />");
            //Response.Write(cacheService.Set<List<Data>>("hello", GetDatas()) + " <br />");

            //var endTime = DateTime.Now;
            //Response.Write("----EndTime：" + endTime + "<br />");
            //Response.Write("----Milliseconds：" + (endTime - startTime).Milliseconds + "<br />");


            ////获取
            //var startTime = DateTime.Now;
            //Response.Write("----GetValue()<br />");
            //Response.Write("----StartTime：" + startTime + "<br />");
            //var list = cacheService.Get<List<Data>>("hello");
            //Response.Write(" <br />");

            //var endTime = DateTime.Now;
            //Response.Write("----EndTime：" + endTime + "<br />");
            //Response.Write("----Milliseconds：" + (endTime - startTime).Milliseconds + "<br />");


            //Keys
            //Response.Write("获取所有的Key：");
            //var keys = cacheService.GetAllKeys();
            //foreach (var key in keys)
            //    Response.Write("Key：" + key + "<br />");

            //cacheService.RemoveByPrefix("WebPc.Pages");

            //Response.Write("通过表达式获取所有的Key：【WebPc.Pages*】");
            //var keys = cacheService.GetKeysByPrefix("WebPc.Pages");
            //foreach (var key in keys)
            //    Response.Write("Key：" + key + "<br />");

            //ICacheManager cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
            //var cache = cacheManager.GetCache();
            //cache.Set("sayhi", "hello world2");

            //Response.Write(cache.GetOrDefault("sayhi"));

            //URedisCacheManager cacheManger = UPrimeEngine.Instance.Resolve<URedisCacheManager>();
            //((URedisCache)cacheManger.GetCache()).FlushDb();
            //for (var i = 0; i < 1000; i++) {
            //    cacheManger.GetCache().Set("h" + i, System.IO.Path.GetRandomFileName());
            //}

            //cacheManger.GetCache().RemoveByPattern("h");
                //cacheManger.GetCache().Set("heyben1", "haha123");
                //cacheManger.FlushDb();
                //ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("106.75.20.98:19000,proxy=Twemproxy");
                //var db = conn.GetDatabase();
                //db.StringSet("h2", "h3");
            var cacheManager = UPrimeEngine.Instance.Resolve<ICacheManager>();
                Response.Write("success");


        }

        


        public class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string EnName { get; set; }

            public string Domain { get; set; }

            public string QQGroup { get; set; }

            public int Sort { get; set; }

            public string Description { get; set; }
        }
    }
}
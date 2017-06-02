using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceStack.Redis;
using U.Dependency;


namespace U.Tests.Web.BaseService.DistributedCache
{
    public partial class Redis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           // IDistributedCacheManager manager = UPrime.Instance.Resolve<IDistributedCacheManager>();
          


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
            //Response.Write(manager.Set<List<Data>>("hello", GetDatas()) + " <br />");

            //var endTime = DateTime.Now;
            //Response.Write("----EndTime：" + endTime + "<br />");
            //Response.Write("----Milliseconds：" + (endTime - startTime).Milliseconds + "<br /><br /><br /><br />");


            ////获取
            //startTime = DateTime.Now;
            //Response.Write("----GetValue()<br />");
            //Response.Write("----StartTime：" + startTime + "<br />");
            //var list = manager.Get<List<Data>>("hello");
            //Response.Write(" <br />");

            //endTime = DateTime.Now;
            //Response.Write("----EndTime：" + endTime + "<br />");
            //Response.Write("----Milliseconds：" + (endTime - startTime).Milliseconds + "<br /><br /><br /><br />");


            ////Keys
            //Response.Write("获取所有的Key：");
            //var keys = manager.GetAllKeys();
            //foreach (var key in keys)
            //    Response.Write("Key：" + key + "<br /><br /><br /><br />");

            //manager.RemoveByPrefix("");

            //Response.Write("通过表达式获取所有的Key：【WebPc.Pages*】");
            //keys = manager.GetKeysByPrefix("WebPc.Pages");
            //foreach (var key in keys)
            //    Response.Write("Key：" + key + "<br />");

        }

        List<Data> GetDatas()
        {
            var list = new List<Data>();
            for (var i = 0; i < 100; i++)
            {
                list.Add(new Data()
                {
                    Id = i,
                    Sort = i,
                    Description = "Description海",
                    Domain = "http://js.youzy.cn",
                    Name = "江苏" + i,
                    EnName = "js",
                    QQGroup = "21235512122135412"
                });
            }

            return list;
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
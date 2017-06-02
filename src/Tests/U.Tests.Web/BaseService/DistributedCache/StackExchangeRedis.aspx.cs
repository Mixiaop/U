using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StackExchange.Redis;
namespace U.Tests.Web.BaseService.DistributedCache
{
    public partial class StackExchangeRedis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var conn = ConnectionMultiplexer.Connect("120.132.94.138:6379");

            var server = conn.GetServer("120.132.94.138:6379");
            var db = conn.GetDatabase(0);

            var keys = server.Keys(0, "Youzy.LocalTest*").ToList();

            foreach (var k in keys) {
                Response.Write(k);
            }

            Response.Write(db.KeyExists("Youzy.LocalTests$hello"));
            //IDatabase db = conn.GetDatabase();

            //var data = db.StringGet("Youzy.Release$WebPc.Pages.HomePage-nx");
            //db.get
           // var a = 1;
            
        }
    }
}
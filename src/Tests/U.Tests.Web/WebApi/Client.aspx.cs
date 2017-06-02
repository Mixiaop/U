using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using U.WebApi.Client;
using U.WebApi.Models;

namespace U.Tests.Web.WebApi
{
    public partial class Client : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IUWebApiClient client = UPrimeEngine.Instance.Resolve<IUWebApiClient>();
                client.BaseUrl = "http://123.59.57.18:8018/";
                var task = await client.GetAsync<IList<AppVersionModel>>("/app/version/android");
                Response.Write(task[0].Body);

                //using (var client = new HttpClient())
                //{

                //    var result = client.GetAsync("http://123.59.57.18:8018/app/version/android").Result.Content.ReadAsStringAsync();
                //Response.Write(result);

                    
                //}
            }
            catch (Exception ex) {
                var a = 1;
            }
        }
    }

    public class AppVersionModel {
        public string VersionCode { get; set; }

        public string Summary { get; set; }

        public string Body { get; set; }

        public int IsCompel { get; set; }

        public string UpdateDate { get; set; }

        public string UpdateUrl { get; set; }
    }
}
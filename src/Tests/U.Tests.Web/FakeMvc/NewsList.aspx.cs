using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.FakeMvc.UI;
using U.FakeMvc.Routes;

namespace U.Tests.Web.FakeMvc
{
    public partial class NewsList : PageBase<NewsController, NewsListModel>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            Response.Write(Model.Title);

            Response.Write(RouteContext.Instance.GetRouteUrl("Index", 1, 1));
        }

        void btnSave_Click(object sender, EventArgs e)
        {
        }
    }
}
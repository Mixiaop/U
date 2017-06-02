using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U.Tests.Web.AsposeWords
{
    public partial class Result : System.Web.UI.Page
    {
        protected string StrResult = "技术型(TF型),管理型(GM型),自主型(AU型),安全型(SE型),创造型(SC型),服务型(SV型),挑战型(CH型),生活型(IS型)";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(5000);
        }
    }
}
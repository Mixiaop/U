using System;
using System.Diagnostics;
using U.Utilities.IIS;
using U.Utilities.Web;
using U.Utilities;

namespace U.Web.IISManage
{
    public partial class _Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var process = Process.GetProcessesByName("w3wp");
            foreach (Process p in process)
            {
                var username = U.Utilities.CommonHelper.GetProcessUsername(p.Id);
                Response.Write(username + "<br />");
            }

        }
    }
}
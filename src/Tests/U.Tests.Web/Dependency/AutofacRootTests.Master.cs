using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace U.Tests.Web.Dependency
{
    public partial class AutofacRootTests : System.Web.UI.MasterPage
    {
        //IWorkContext _workContext = UPrimeEngine.Instance.Resolve<IWorkContext>();
        IWorkContext _workContext = new WebWorkContext();
        public string CurrentProvince;
        protected void Page_Load(object sender, EventArgs e)
        {
            //CurrentProvince = _workContext.CurrentProvince;
            CurrentProvince = ((AutofacPageBase)this.Page).CurrentProvince;
        }
    }
}
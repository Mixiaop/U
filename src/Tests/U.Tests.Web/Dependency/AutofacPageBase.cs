using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Utilities.Web;
namespace U.Tests.Web.Dependency
{
    public abstract class AutofacPageBase : System.Web.UI.Page
    {
        public IWorkContext WorkContext = UPrimeEngine.Instance.Resolve<IWorkContext>();

        public string CurrentProvince;
        public AutofacPageBase() { 
            var versionEnName = WebHelper.GetString("p");
            if (!string.IsNullOrEmpty(versionEnName) &&  versionEnName != WorkContext.CurrentProvince)
            {
                var version = versionEnName;
                if (version != null)
                {
                    WorkContext.CurrentProvince = version;
                }
            }

            CurrentProvince = WorkContext.CurrentProvince;
        }
    }
}
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U;
using U.Domain.Repositories;
using U.EntityFramework;
using Autofac.Builder;
using Autofac;
using U.Dependency.AutofacUtils.DynamicProxy2;
using U.Dependency;
using U.EntityFramework.Startup.Configuration;

namespace U.Tests.Web.EntityFramework
{
    public partial class RepositoryTests2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IAreaRepository areaRepository = UPrimeEngine.Instance.Resolve<IAreaRepository>();
            var area = areaRepository.Get(4);
            Response.Write(area.AreaName);
        }
    }
}
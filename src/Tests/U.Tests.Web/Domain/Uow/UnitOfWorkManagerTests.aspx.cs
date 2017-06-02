using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityFramework.DynamicFilters;
using U.EntityFramework;
using U.Domain.Uow;
using U.Tests.Web.EntityFramework;

namespace U.Tests.Web.Domain.Uow
{
    public partial class UnitOfWorkManagerTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUnitOfWorkManager uowManager = UPrimeEngine.Instance.Resolve<IUnitOfWorkManager>();

            var current = uowManager.Current;
            IAreaRepository areaRepository = UPrimeEngine.Instance.Resolve<IAreaRepository>();

            
            //var a = uowManager;
            using (var context = new MyContext("")) {
                context.DisableFilter(U.Domain.Uow.UDataFilters.SoftDelete);
                var area = context.Areas.FirstOrDefault(x => x.Id == 2);
                context.Areas.Remove(area);
                context.NativeSaveChanges();
            }

            var area2 = areaRepository.GetAll().FirstOrDefault(x => x.Id == 1);

            var a = 1;
        }
    }
}
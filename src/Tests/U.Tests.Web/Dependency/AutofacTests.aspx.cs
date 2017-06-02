using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using U.Dependency;

namespace U.Tests.Web.Dependency
{
    public partial class AutofacTests : AutofacPageBase
    {
        protected IContainer Container { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //var builder = new ContainerBuilder();
            //builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //Container = builder.Build();

            //var scope = Container;

            //var workContext1 = Container.Resolve<IWorkContext>();
            //workContext1.Value = 1;

            //var workContext2 = Container.Resolve<IWorkContext>();

            //Response.Write("workContext2：" + workContext2.Value);
            //Response.Write("---------------");
        }

        private ILifetimeScope Scope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                return Container.BeginLifetimeScope();
            }
            catch (Exception exc)
            {
                return Container.BeginLifetimeScope();
            }
        }
    }



    //public interface IWorkContext
    //{
    //    int Value { get; set; }
    //}

    //public class WebWorkContext : IWorkContext
    //{
    //    private int _value;
    //    public int Value
    //    {
    //        get
    //        {
    //            return _value;
    //        }
    //        set { _value = value; }
    //    }
    //}
}
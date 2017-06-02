using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using U;
using U.Dependency;

namespace U.Tests.Web.Dependency
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnTest.Click += btnTest_Click;
            //UPrime.Instance.Register<IHello, Hello>(DependencyLifeStyle.Transient);
            //UPrime.Instance.IocManager.IocBuilder.Update(UPrime.Instance.IocManager.IocContainer);
            //解析对象
            //var hello = UPrime.Instance.Resolve<IHello>();
            //Response.Write(hello.SayHi());


            //var hello2 = container.Resolve<IHello>();

            //var typeFinder = container.Resolve<ITypeFinder>();
            //Response.Write(hello2.SayHi());
            //var info = new ProfilerConfigInfo();
            //info.Apply = true;
            //ProfilerConfigs.SaveConfig(info);

            //var a = UPrimeEngine.Instance.Configuration;

            if (!IsPostBack)
            {
                var hello = UPrimeEngine.Instance.Resolve<IHello>();

                Response.Write(hello.SayHi());
                hello.Add("【haha2】");

                var hello2 = UPrimeEngine.Instance.Resolve<IHello>();
                Response.Write(hello2.SayHi());
            }
        }

        void btnTest_Click(object sender, EventArgs e)
        {
            var hello2 = UPrimeEngine.Instance.Resolve<IHello>();
            Response.Write(hello2.SayHi());
        }
    }

    public interface IHello : ITransientDependency
    {
        string SayHi();
        void Add(string str);
    }

    public class Hello : IHello
    {
        private string message = "hello world";
        public string SayHi()
        {
            return message;
        }

        public void Add(string str)
        {
            message += str;
        }
    }
}
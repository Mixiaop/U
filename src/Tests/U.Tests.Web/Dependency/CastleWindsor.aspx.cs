using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using U.Settings;
using U.Net.Mail;
//using Castle.Windsor;
//using Castle.MicroKernel.Registration;

namespace U.Tests.Web.Dependency
{
    public partial class CastleWindsor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //var container = new WindsorContainer();

            //container.Register(

            //    Component.For<ICalculatorService>().ImplementedBy<CalculatorService>()
            //    );


            //var service = container.Resolve<ICalculatorService>();

            //Response.Write(service.Add(1, 1));


            //container.Register(
            //     Classes.FromThisAssembly()
            //        .BasedOn<USettings>()
            //        .LifestyleSingleton()
            //        .Configure(c=> 
            //    );

            //var settings = container.Resolve<USettings>();
            //var a = 1;

            ISettingsManager settingsManager = UPrimeEngine.Instance.Resolve<ISettingsManager>();
            var setttings = settingsManager.GetSettings<EmailSenderSettings>();
            var a = 1;
        }


    }

    public abstract class SettingsBase
    {

        public abstract string Name { get; set; }
    }

    public class TestSettings : SettingsBase
    {
        public TestSettings()
        {
            Name = "TestSettings";
        }
        public override string Name { get; set; }
    }

    public class SettingManager<T> where T : SettingsBase
    {
        public T Resolve()
        {
            return default(T);
        }
    }


    public interface IService { }

    public interface ICalculatorService : IService
    {
        float Add(float op1, float op2);
    }

    public class CalculatorService : ICalculatorService
    {
        public float Add(float op1, float op2)
        {
            return op1 + op2;
        }
    }
}
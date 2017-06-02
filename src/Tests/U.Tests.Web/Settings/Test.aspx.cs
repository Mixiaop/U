using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using U.Settings;
using U.Net.Mail;
using U.Tests.Web.Infrastructure;

namespace U.Tests.Web.Settings
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnClear.Click += btnClear_Click;
            ISettingsManager settingsManager = UPrimeEngine.Instance.Resolve<ISettingsManager>();

           // var settings = new EmailSenderSettings();
            //var settings = settingsManager.GetSettings<EmailSenderSettings>();

            //settings.UpdateSettings(settings);
            //for (int i = 0; i <= 100; i++)
            //{
            //    var settings = UPrimeEngine.Instance.Resolve<SoaClientImgSettings>();
            //    Response.Write(settings.Url);
            //}
            
            //settings.DefaultFromDisplayName += "hello world";
            //settingsManager.SaveSettings(settings);
            //settings.UpdateSettings(settings);

            //settings = UPrime.Instance.Resolve<EmailSenderSettings>();
            //Response.Write(settings.DefaultFromDisplayName);

            //var settings = new TestSettings();
            
            //settings.UpdateSettings(settings);
            var settings = UPrimeEngine.Instance.Resolve<TestSettings>();
            Response.Write(settings.Name);
            Response.Write("<br />");

            settings.Name = "helloworld";
            settingsManager.SaveSettings<TestSettings>(settings);

            var settings2 = UPrimeEngine.Instance.Resolve<TestSettings>();
            Response.Write(settings2.Name);
            Response.Write("<br />");

            //var a = 1;
        }

        void btnClear_Click(object sender, EventArgs e)
        {
            ISettingsManager settingsManager = UPrimeEngine.Instance.Resolve<ISettingsManager>();
            settingsManager.ResetAllSettings();
            Response.Write("清空成功");

            
        }
    }

    public class SettingsTest { 
        EmailSenderSettings _settings;
        public SettingsTest(EmailSenderSettings settings) {
            _settings = settings;
            //var a = 1;
        }
    }
}
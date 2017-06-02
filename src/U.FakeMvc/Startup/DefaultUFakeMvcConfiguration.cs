
namespace U.FakeMvc.Startup
{
    public class DefaultUFakeMvcConfiguration : IUFakeMvcConfiguration
    {
        public string AjaxProGenerateScriptsPath
        {
            get { return "/js/ajaxservices.js"; }
        }
    }
}

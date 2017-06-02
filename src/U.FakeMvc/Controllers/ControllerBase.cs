using System.Web;
using System.Web.UI;
using U.FakeMvc.Models;

namespace U.FakeMvc.Controllers
{
    public abstract class ControllerBase : System.Web.UI.Page
    {

        public ControllerBase()
        {

        }

        public HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public virtual void Invoke404()
        {
            Response.Redirect("/404.html");
        }

        public virtual void Invoke500()
        {
            Response.Redirect("/500.html");
        }

        public TController Current<TController>() where TController : ControllerBase
        {
            return (TController)this;
        }
    }

    ///// <summary>
    ///// ASPX.Page继承此类，并实现初始化视图，替换UI.PageBase类
    ///// </summary>
    ///// <typeparam name="TModel"></typeparam>
    //public abstract class ControllerBase<TModel> : ControllerBase {
    //    private TModel _model;

    //    public abstract TModel InitView();

    //    public ControllerBase() {
    //        _model = InitView();
    //    }

    //    public TModel Model { get { return _model; } }
    //}

}

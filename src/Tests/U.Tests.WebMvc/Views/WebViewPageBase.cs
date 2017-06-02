using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using U.Web.Mvc.Views;
namespace U.Tests.WebMvc.Views
{
    public abstract class WebViewPageBase : UTestsWebMvcWebViewPageBase<dynamic>
    {

    }

    public abstract class UTestsWebMvcWebViewPageBase<TModel> : UWebViewPage<TModel>
    {
        protected UTestsWebMvcWebViewPageBase()
        {
        }
    }
}
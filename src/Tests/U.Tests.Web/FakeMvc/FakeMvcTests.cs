using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using U.FakeMvc;
using U.FakeMvc.Controllers;
using U.FakeMvc.Models;

namespace U.Tests.Web.FakeMvc
{
    public class NewsController : ControllerBase
    {
        [HttpGet]
        public NewsListModel NewsList(string keywords, int movieId)
        {
            var model = new NewsListModel();
            model.Title = "hello world";
            return model;
        }
    }

    //public class NewsAjaxController : ControllerBase
    //{

    //}

    //public class News
    //{

    //}
    public class NewsListModel : ModelBase
    {
        public string Title { get; set; }
    }
}
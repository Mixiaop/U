using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using U.Web.Models;

namespace U.Tests.WebMvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            int i = 0;
            int b = 4 / i;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}
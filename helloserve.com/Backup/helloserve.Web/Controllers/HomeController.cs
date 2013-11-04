using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.Web
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = string.Format("{0} - helloserve Productions", "Home");
            return View(new HomeModel());
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View("Error");
        }
    }
}

using helloserve.com.Azure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Load();
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
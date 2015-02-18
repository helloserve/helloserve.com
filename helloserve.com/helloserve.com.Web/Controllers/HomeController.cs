using helloserve.com.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View(new HomeViewModel());
        }
    }
}
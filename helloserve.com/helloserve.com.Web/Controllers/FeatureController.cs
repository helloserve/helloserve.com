using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
{
    public class FeatureController : BaseController
    {
        // GET: Feature
        public ActionResult Index()
        {
            return View();
        }
    }
}
using helloserve.com.Web.Models;
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
            return View(new BaseViewModel());
        }

        [HttpGet]
        [Route("/Feature/{id:int}")]
        public ActionResult ById(int id)
        {
            return View("Feature", new BaseViewModel());
        }

        [HttpGet]
        [Route("/Feature/{name:string}")]
        public ActionResult ByName(string name)
        {
            return View("Feature", new BaseViewModel());
        }
    }
}
using helloserve.com.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog
        public ActionResult Index()
        {
            BlogViewModel model = new BlogViewModel();
            model.Load();
            return View(model);
        }
    }
}
using helloserve.com.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Projects()
        {
            ProjectsViewModel model = new ProjectsViewModel();
            model.Load();
            return View(model);
        }

        public ActionResult Blogs()
        {
            BlogViewModel model = new BlogViewModel();
            model.Load();
            return View(model);
        }
    }
}
using helloserve.com.Web.Models;
using helloserve.com.Web.Models.Data;
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

        public ActionResult Project(int? id)
        {
            ProjectDataModel model = new ProjectDataModel();
            if (id.HasValue)
                model = Model.Feature.Get(id.Value).AsDataModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Project(ProjectDataModel model)
        {
            model.Save();
            return View(model);
        }

        public ActionResult Blogs()
        {
            BlogViewModel model = new BlogViewModel();
            model.Load();
            return View(model);
        }

        public ActionResult Blog(int? id)
        {
            NewsDataModel model = new NewsDataModel();
            if (id.HasValue)
                model = Model.News.Get(id.Value).AsDataModel();

            model.LoadForEdit();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Blog(NewsDataModel model)
        {
            model.Save();
            model.LoadForEdit();
            return View(model);
        }

        public ActionResult Media()
        {
            MediaViewModel model = new MediaViewModel();
            return View(model);
        }
    }
}
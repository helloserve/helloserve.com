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
            if (!IsAuthenticated())
                return Authenticate();

            return View();
        }

        public ActionResult Projects()
        {
            if (!IsAuthenticated())
                return Authenticate();

            ProjectsViewModel model = new ProjectsViewModel();
            model.Load();
            return View(model);
        }

        public ActionResult Project(int? id)
        {
            if (!IsAuthenticated())
                return Authenticate();

            ProjectDataModel model = new ProjectDataModel();
            if (id.HasValue)
                model = Model.Feature.Get(id.Value).AsDataModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Project(ProjectDataModel model)
        {
            if (!IsAuthenticated())
                return Authenticate();

            model.Save();
            return View(model);
        }

        public ActionResult Blogs()
        {
            if (!IsAuthenticated())
                return Authenticate();

            BlogEditModel model = new BlogEditModel();
            model.Load();
            return View(model);
        }

        public ActionResult Blog(int? id)
        {
            if (!IsAuthenticated())
                return Authenticate();

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
            if (!IsAuthenticated())
                return Authenticate();

            model.Save();
            model.LoadForEdit();
            return View(model);
        }

        public ActionResult Media()
        {
            if (!IsAuthenticated())
                return Authenticate();

            MediaViewModel model = new MediaViewModel();
            return View(model);
        }

        private bool IsAuthenticated()
        {
            return AdminSession.Instance.Authenticated;
        }

        private ActionResult Authenticate()
        {
            AdminSession.Instance.RedirectPath = HttpContext.Request.Url.AbsoluteUri;
            return View("Authenticate", new UserDataModel());
        }

        [HttpPost]
        public ActionResult Authenticate(UserDataModel model)
        {
            if (ModelState.IsValid)
            {
                AdminSession.Instance.Authenticated = model.Authenticate();
            }

            string redirectUri = AdminSession.Instance.RedirectPath;
            if (!string.IsNullOrEmpty(redirectUri))
                return Redirect(redirectUri);

            return RedirectToAction("Index");
        }
    }
}
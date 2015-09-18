using helloserve.com.Azure.Models;
using helloserve.com.Azure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Controllers
{
    public class ProjectController : BaseController
    {
        // GET: Feature
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ProjectsViewModel model = new ProjectsViewModel();
                model.Load();
                return View(model);
            }
            else
            {
                int idValue;
                if (int.TryParse(id, out idValue))
                    return ById(idValue);
                else
                    return ByName(id);
            }
        }

        private ActionResult ById(int id)
        {
            ProjectsViewModel baseModel = new ProjectsViewModel();
            baseModel.Load();

            ProjectDataModel model = baseModel.Projects.GetById(id) as ProjectDataModel;            
            if (model == null)
                model = new ProjectDataModel();
            model.Load();
            model.LoadForView();

            SetColors(model);

            return View("Project", model);
        }

        private ActionResult ByName(string name)
        {
            ProjectsViewModel baseModel = new ProjectsViewModel();
            baseModel.Load();

            ProjectDataModel model = baseModel.Projects.GetByName(name) as ProjectDataModel;
            if (model == null)
                model = new ProjectDataModel();
            model.Load();
            model.LoadForView();

            SetColors(model);

            return View("Project", model);
        }

        protected override void SetColors(BaseViewModel model)
        {
            base.SetColors(model);

            ProjectDataModel dataModel = model as ProjectDataModel;
            ViewBag.Color = dataModel.Color;
            ViewBag.BackgroundColor = dataModel.BackgroundColor;
            ViewBag.LinkColor = dataModel.LinkColor;
            ViewBag.LinkHoverColor = dataModel.LinkHoverColor;
            ViewBag.HeaderLinkColor = dataModel.HeaderLinkColor ?? dataModel.LinkColor;
            ViewBag.HeaderLinkHoverColor = dataModel.HeaderLinkHoverColor ?? dataModel.LinkHoverColor;
        }
    }
}
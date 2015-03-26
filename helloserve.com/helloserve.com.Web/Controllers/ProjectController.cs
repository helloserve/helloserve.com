using helloserve.com.Web.Models;
using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
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

            return View("Project", model);
        }
    }
}
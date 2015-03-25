using helloserve.com.Web.Models;
using helloserve.com.Web.Models.Data;
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
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View(new BaseViewModel());
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
            BaseViewModel baseModel = new BaseViewModel();
            baseModel.Load();

            FeatureDataModel model = baseModel.Features.GetById(id) as FeatureDataModel;            
            if (model == null)
                model = new FeatureDataModel();
            model.Load();

            return View("Feature", model);
        }

        private ActionResult ByName(string name)
        {
            BaseViewModel baseModel = new BaseViewModel();
            baseModel.Load();

            FeatureDataModel model = baseModel.Features.GetByName(name) as FeatureDataModel;
            if (model == null)
                model = new FeatureDataModel();
            model.Load();

            return View("Feature", model);
        }
    }
}
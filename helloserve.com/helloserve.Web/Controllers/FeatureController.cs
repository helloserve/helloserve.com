using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.Web.Controllers
{
    public class FeatureController : BaseController
    {
        public ActionResult Index()
        {
            FeaturesModel model = new FeaturesModel();
            return View(model);
        }

        public ActionResult Feature(string id)
        {
            int featureID = 0;
            FeatureModel model = null;

            if (int.TryParse(id, out featureID))
            {
                model = new FeatureModel(featureID);
                return View(model);
            }

            model = FeatureModel.FromSubdomain(id);
            if (model != null)
                return View(model);

            return null;
        }
    }
}
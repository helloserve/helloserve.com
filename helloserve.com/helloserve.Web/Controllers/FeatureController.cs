using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Common;

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
                //LogRepo.LogForFeature(Settings.Current.GetUserID(), featureID, model.Feature.Name, "Feature.Feature");
                Settings.EventLogger.Log(EventLogEntry.LogForFeature(Settings.Current.GetUserID(), featureID, model.Feature.Name, "Feature.Feature"));
                return View(model);
            }

            return FromSubdomain(id);
        }

        public ActionResult FromSubdomain(string id)
        {
            FeatureModel model = FeatureModel.FromSubdomain(id);
            if (model != null)
            {
                //LogRepo.LogForFeature(Settings.Current.GetUserID(), model.Feature.FeatureID, model.Feature.Name, "Feature.FromSubdomain");
                Settings.EventLogger.Log(EventLogEntry.LogForFeature(Settings.Current.GetUserID(), model.Feature.FeatureID, model.Feature.Name, "Feature.Feature"));
                return View(model);
            }

            return null;
        }
    }
}
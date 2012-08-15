using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Web;
using helloserve.Common;

namespace helloserve.Web
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Admin()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.Admin - What?");

            AdminMenuModel model = new AdminMenuModel();
            return View(model);
        }

        public ActionResult AdminFunctions()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AdminFunctions - What?");

            AdminMenuModel model = new AdminMenuModel();
            return PartialView("_AdminFunctions", model);
        }

        public ActionResult AddFeature()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddFeature - What?");

            return PartialView("_Feature", new AdminFeatureModel());
        }

        public ActionResult EditFeature(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditFeature - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                return PartialView("_Feature", new AdminFeatureModel(featureID));
            }

            return null;
        }

        public ActionResult SaveFeature(FormCollection form)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveFeature - What?");

            try
            {
                int featureID = 0;
                AdminFeatureModel model;
                if (int.TryParse(form["Feature.FeatureID"], out featureID))
                {
                    if (featureID == 0)
                        model = new AdminFeatureModel();
                    else
                        model = new AdminFeatureModel(featureID);
                }
                else
                    return ReturnJsonException(new Exception(), "Save Feature - Invalid Key");

                TryUpdateModel<Feature>(model.Feature, "Feature");

                if (!ModelState.IsValid)
                {
                    return ReturnJsonResult(true, this.RenderPartialView("_Feature", model));
                }
                else
                {
                    model.Feature.Save();
                    return View("Admin", new AdminMenuModel());
                }
            }
            catch (Exception ex)
            {
                return ReturnJsonException(ex, ex.Message);
            }
        }

        public ActionResult DeleteFeature(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DeleteFeature - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                AdminFeatureModel model = new AdminFeatureModel(featureID);
                model.Feature.Delete();
            }

            return null;
        }
    }
}

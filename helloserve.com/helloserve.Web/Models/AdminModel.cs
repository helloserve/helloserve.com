using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Common;

namespace helloserve.Web
{
    public class AdminMenuModel
    {
        public List<SelectListItem> EditFeatureList { get; set; }

        public AdminMenuModel()
        {
            EditFeatureList = FeatureRepo.GetAll().Select(f=>new SelectListItem() { Text = f.Name, Value = f.FeatureID.ToString() }).ToList();
        }
    }

    public class AdminFeatureModel
    {
        public Feature Feature;

        public AdminFeatureModel()
        {
            Feature = FeatureRepo.GetNew();
        }

        public AdminFeatureModel(int id)
        {
            Feature = FeatureRepo.GetByID(id);
        }
    }
}
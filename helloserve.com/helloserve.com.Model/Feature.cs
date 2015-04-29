using helloserve.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public class Feature : Entities.Feature
    {
        public static List<Feature> GetAll()
        {
            FeatureRepository repo = new FeatureRepository();
            return repo.GetAll().OrderByDescending(f => f.IsMainFeature).ThenByDescending(f => f.CreatedDate).ToModelList();            
        }

        public static Feature Get(int featureId)
        {
            FeatureRepository repo = new FeatureRepository();
            return repo.Get(featureId).AsModel();
        }

        public static void Save(Feature model)
        {
            if (model.CreatedDate == DateTime.MinValue)
                model.CreatedDate = DateTime.UtcNow;

            model.ModifiedDate = DateTime.UtcNow;

            FeatureRepository repo = new FeatureRepository();
            Entities.Feature entity = repo.Update(model.FeatureID, model.Name, model.Description, model.ExtendedDescription, model.CreatedDate, model.ModifiedDate, model.HeaderImageID, model.MediaFolder, model.Subdomain, model.CustomPage, model.IsMainFeature, model.Color, model.BackgroundColor, model.LinkColor, model.LinkHoverColor, model.HeaderLinkColor, model.HeaderLinkHoverColor);

            model.FeatureID = entity.FeatureID;
        }
    }
}

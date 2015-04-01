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
    }
}

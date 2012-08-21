using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class FeatureRepo : BaseRepo<Feature>
    {
        public static Feature GetByID(int featureID)
        {
            var feature = (from f in DB.Features
                           where f.FeatureID == featureID
                           select f).SingleOrDefault();

            return feature;
        }

        public static Feature GetBySubdomain(string domain)
        {
            var feature = (from f in DB.Features
                           where f.Subdomain.Contains(domain)
                           select f).SingleOrDefault();

            return feature;
        }

        public static Feature GetNew()
        {
            Feature feature = new Feature()
            {
                Name = "New Feature",
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            return feature;
        }

        public static IEnumerable<FeatureRequirementModel> GetRequirements(int featureID)
        {
            var data = (from f in DB.Features
                        join fr in DB.FeatureRequirements on f.FeatureID equals fr.FeatureID
                        join r in DB.Requirements on fr.RequirementID equals r.RequirementID
                        where f.FeatureID == featureID
                        select new FeatureRequirementModel
                        {
                            FeatureRequirement = fr,
                            Requirement = r
                        });

            return data;
        }
    }
}

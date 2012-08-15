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
    }
}

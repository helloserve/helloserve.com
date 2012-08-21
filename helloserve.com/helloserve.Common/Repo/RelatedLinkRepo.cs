using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class RelatedLinkRepo : BaseRepo<RelatedLink>
    {
        public static RelatedLink GetByID(int id)
        {
            var data = DB.RealtedLinks.Where(r => r.RelatedLinkID == id).SingleOrDefault();

            return data;
        }

        public static IEnumerable<RelatedLink> GetFeatureLinks(int featureID)
        {
            var data = from r in DB.RealtedLinks
                       where r.FeatureID == featureID
                       select r;

            return data;
        }
    }
}

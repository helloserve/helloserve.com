using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class FeatureMediaRepo : BaseRepo<FeatureMedia>
    {
        public static IEnumerable<FeatureMedia> GetFeatureMediaLink(int featureID)
        {
            var data = from l in DB.FeatureMediaItems
                       where l.FeatureID == featureID
                       select l;

            return data;
        }

        public static FeatureMedia Link(int featureID, int mediaID)
        {
            FeatureMedia link = (from l in DB.FeatureMediaItems
                                 where l.FeatureID == featureID && l.MediaID == mediaID
                                 select l).SingleOrDefault();

            if (link == null)
            {
                link = new FeatureMedia()
                {
                    FeatureID = featureID,
                    MediaID = mediaID
                };

                link.Save();
            }

            return link;
        }
    }
}

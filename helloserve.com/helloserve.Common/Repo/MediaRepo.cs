using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class MediaRepo : BaseRepo<Media>
    {
        public static Media GetByID(int mediaID)
        {
            var media = (from m in DB.MediaItems
                         where m.MediaID == mediaID
                         select m).SingleOrDefault();

            return media;
        }

        public static IEnumerable<Media> GetMediaForFeature(int featureID)
        {
            var media = (from m in DB.MediaItems
                         join fm in DB.FeatureMediaItems on m.MediaID equals fm.MediaID
                         where fm.FeatureID == featureID
                         select m);

            return media;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public static class MediaExtensions
    {
        public static Media AsModel(this Entities.Media entity)
        {
            return new Media()
            {
                MediaID = entity.MediaID,
                MediaType = entity.MediaType,
                FileName = entity.FileName,
                Width = entity.Width,
                Height = entity.Height,
                Location = entity.Location,
                DateAdded = entity.DateAdded
            };
        }

        public static List<Media> ToModelList(this IEnumerable<Entities.Media> collection)
        {
            List<Media> list = new List<Media>();
            foreach (Entities.Media entity in collection)
            {
                list.Add(entity.AsModel());
            }
            return list;
        }
    }
}

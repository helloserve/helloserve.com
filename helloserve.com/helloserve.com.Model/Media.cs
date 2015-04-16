using helloserve.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public class Media : Entities.Media
    {
        public static Media Get(int? mediaId)
        {
            if (!mediaId.HasValue)
                return new Media()
                {
                    FileName = "placeholder-200x150.png"
                };

            MediaRepository repo = new MediaRepository();
            return repo.Get(mediaId.Value).AsModel();
        }

        public static List<Media> GetAll()
        {
            MediaRepository repo = new MediaRepository();
            return repo.GetAll().ToModelList();
        }

        public static Media Add(string relativePath, int width, int height)
        {
            MediaRepository repo = new MediaRepository();
            int mediaType = 1;

            return repo.Add(relativePath, mediaType, width, height).AsModel();
        }
    }
}

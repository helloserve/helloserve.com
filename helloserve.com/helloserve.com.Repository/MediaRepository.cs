using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class MediaRepository : Base.BaseRepository
    {
        public IQueryable<Media> GetAll()
        {
            return Db.Media1;
        }

        public Media Get(int mediaId)
        {
            return Db.Media1.Single(m => m.MediaID == mediaId);
        }

        public Media Add(string relativePath, int mediaType, int width, int height)
        {
            Media media = new Media()
            {
                Location = relativePath,
                FileName = Path.GetFileName(relativePath),
                MediaType = mediaType,
                Width = width,
                Height = height,
                DateAdded = DateTime.UtcNow
            };

            Db.Media1.Add(media);

            Db.SaveChanges();

            return media;
        }
    }
}

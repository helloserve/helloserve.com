using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class MediaRepository : Base.BaseRepository
    {
        public Media Get(int mediaId)
        {
            return Db.Medias.Single(m => m.MediaID == mediaId);
        }
    }
}

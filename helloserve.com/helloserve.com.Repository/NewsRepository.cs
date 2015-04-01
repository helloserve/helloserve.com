using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class NewsRepository : Base.BaseRepository
    {
        public IQueryable<News> GetAll()
        {
            return Db.News;
        }
    }
}

using helloserve.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public class News : Entities.News
    {
        public static List<News> GetAll()
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().OrderByDescending(n=>n.CreatedDate).ToModelList();
        }

        public static News GetLatest()
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().OrderByDescending(n => n.CreatedDate).FirstOrDefault().AsModel();
        }
    }
}

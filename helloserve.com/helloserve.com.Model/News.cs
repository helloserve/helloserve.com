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
            return repo.GetAll().OrderByDescending(n => n.CreatedDate).ToModelList();
        }

        public static News GetLatest()
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().OrderByDescending(n => n.CreatedDate).FirstOrDefault().AsModel();
        }

        public static News Get(int id)
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().Single(n => n.NewsID == id).AsModel();
        }

        public static void Save(News model)
        {
            if (model.CreatedDate == DateTime.MinValue)
                model.CreatedDate = DateTime.UtcNow;

            model.ModifiedDate = DateTime.UtcNow;

            NewsRepository repo = new NewsRepository();
            Entities.News entity = repo.Update(model.NewsID, model.FeatureID, model.Title, model.Cut, model.Post, model.CreatedDate, model.ModifiedDate, model.HeaderImageID);

            model.NewsID = entity.NewsID;
        }
    }
}

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

        public News Update(int newsId, int? featureId, string title, string cut, string post, DateTime createdDate, DateTime modifiedDate, int? headerImageId)
        {
            News news = Db.News.SingleOrDefault(n => n.NewsID == newsId);

            if (news == null)
            {
                news = new News();
                Db.News.Add(news);
            }

            news.FeatureID = featureId;
            news.Title = title;
            news.Cut = cut;
            news.Post = post;
            news.CreatedDate = createdDate;
            news.ModifiedDate = modifiedDate;
            news.HeaderImageID = headerImageId;

            Db.SaveChanges();

            return news;
        }
    }
}

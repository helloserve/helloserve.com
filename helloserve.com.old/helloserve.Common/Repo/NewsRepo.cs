using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class NewsRepo : BaseRepo<News>
    {
        public static IEnumerable<News> GetAllNews()
        {
            var items = from n in DB.NewsItems
                        where !n.FeatureID.HasValue
                        select n;

            return items;
        }

        public static IEnumerable<News> GetAllBlogPosts()
        {
            var items = from n in DB.NewsItems
                        where n.FeatureID.HasValue
                        select n;

            return items;
        }

        public static IEnumerable<News> GetBlogPosts(int featureID)
        {
            var items = from n in DB.NewsItems
                        where n.FeatureID == featureID
                        select n;

            return items;
        }

        public static News GetByID(int id)
        {
            var news = (from n in DB.NewsItems
                        where n.NewsID == id
                        select n).Single();

            return news;
        }

        public static News GetNew(int? featureID)
        {
            News news = new News()
            {
                Title = "New News Post",
                FeatureID = featureID,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            return news;
        }

        public static News GetPrev(int newsID)
        {
            var news = (from n in DB.NewsItems
                        join nprev in DB.NewsItems on n.FeatureID equals nprev.FeatureID
                        where n.NewsID == newsID && (nprev.CreatedDate < n.CreatedDate || (nprev.CreatedDate == n.CreatedDate && nprev.NewsID < n.NewsID))
                        orderby new { nprev.CreatedDate, nprev.NewsID } descending
                        select nprev).FirstOrDefault();

            return news;
        }

        public static News GetNext(int newsID)
        {
            var news = (from n in DB.NewsItems
                        join nnext in DB.NewsItems on n.FeatureID equals nnext.FeatureID
                        where n.NewsID == newsID && (nnext.CreatedDate > n.CreatedDate || (nnext.CreatedDate == n.CreatedDate && nnext.NewsID > n.NewsID))
                        orderby new { nnext.CreatedDate, nnext.NewsID }
                        select nnext).FirstOrDefault();

            return news;
        }
    }
}

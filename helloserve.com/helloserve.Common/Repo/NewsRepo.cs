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
    }
}

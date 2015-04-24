using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public static class NewsExtensions
    {
        public static News AsModel(this Entities.News entity)
        {
            return new News()
            {
                NewsID = entity.NewsID,
                FeatureID = entity.FeatureID,
                Title = entity.Title,
                Cut = entity.Cut,
                Post = entity.Post,
                HeaderImageID = entity.HeaderImageID,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                IsPublished = entity.IsPublished
            };
        }

        public static List<News> ToModelList(this IEnumerable<Entities.News> collection)
        {
            List<News> list = new List<News>();
            foreach (var entity in collection)
            {
                list.Add(entity.AsModel());
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public static class NewsExtensions
    {
        public static Model.News AsModel(this Entities.News entity)
        {
            return new Model.News()
            {
                NewsID = entity.NewsID,
                FeatureID = entity.FeatureID,
                Title = entity.Title,
                Cut = entity.Cut,
                Post = entity.Post,
                HeaderImageID = entity.HeaderImageID,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate
            };
        }

        public static List<Model.News> ToModelList(this IEnumerable<Entities.News> collection)
        {
            List<Model.News> list = new List<News>();
            foreach (var entity in collection)
            {
                list.Add(entity.AsModel());
            }
            return list;
        }
    }
}

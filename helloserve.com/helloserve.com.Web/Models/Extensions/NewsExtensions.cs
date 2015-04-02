using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public static class NewsExtensions
    {
        public static NewsDataModel AsDataModel(this Model.News model)
        {
            return new NewsDataModel()
            {
                NewsId = model.NewsID,
                ProjectId = model.FeatureID,
                Title = model.Title,
                Cut = model.Cut,
                Content = model.Post,
                CreatedDate = model.CreatedDate,
                HeaderImageUrl = Model.Media.Get(model.HeaderImageID).ImageUrl(),
                Project = model.FeatureID.HasValue ? Model.Feature.Get(model.FeatureID.Value).AsDataModel() : null
            };
        }

        public static CollectionViewModel ToCollectionView(this List<Model.News> list)
        {
            CollectionViewModel collection = new CollectionViewModel();
            collection.Load();
            foreach (var item in list)
            {
                collection.ListItems.Add(item.AsDataModel());
            }
            return collection;
        }
    }
}
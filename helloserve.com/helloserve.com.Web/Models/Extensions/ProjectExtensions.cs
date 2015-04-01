using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public static class ProjectExtensions
    {
        public static ProjectDataModel AsDataModel(this Model.Feature model)
        {
            return new ProjectDataModel()
            {
                Title = model.Name,
                Cut = model.Description,
                Content = model.ExtendedDescription,
                CreatedDate = model.CreatedDate,
                FeatureId = model.FeatureID,
                ///ImageUrl = ...               
            };
        }

        public static CollectionViewModel ToCollectionView(this List<Model.Feature> list)
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
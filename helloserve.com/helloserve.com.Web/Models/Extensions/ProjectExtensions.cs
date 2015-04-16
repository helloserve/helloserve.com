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
                MediaFolder = model.MediaFolder,
                CustomPage = model.CustomPage,
                Subdomain = model.Subdomain,
                IsMainFeature = model.IsMainFeature,
                ImageId = model.HeaderImageID,
                ImageUrl = Model.Media.Get(model.HeaderImageID).ImageUrl(),
                Color = model.Color,
                BackgroundColor = model.BackgroundColor
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

        public static Model.Feature AsModel(this ProjectDataModel dataModel)
        {
            return new Model.Feature()
            {
                FeatureID = dataModel.FeatureId,
                Name = dataModel.Title,
                Description = dataModel.Cut,
                ExtendedDescription = dataModel.Content,
                CreatedDate = dataModel.CreatedDate,
                ModifiedDate = DateTime.UtcNow,
                MediaFolder = dataModel.MediaFolder,
                CustomPage = dataModel.CustomPage,
                Subdomain = dataModel.Subdomain,
                IsMainFeature = dataModel.IsMainFeature,
                HeaderImageID = dataModel.ImageId,
                Color = dataModel.Color,
                BackgroundColor = dataModel.BackgroundColor
            };
        }
    }
}
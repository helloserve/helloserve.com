using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public static class FeatureExtensions
    {
        public static Feature AsModel(this Entities.Feature entity)
        {
            return new Feature()
            {
                FeatureID = entity.FeatureID,
                Name = entity.Name,
                IsMainFeature = entity.IsMainFeature,
                Description = entity.Description,
                ExtendedDescription = entity.ExtendedDescription,
                MediaFolder = entity.MediaFolder,
                HeaderImageID = entity.HeaderImageID,
                Subdomain = entity.Subdomain,
                CustomPage = entity.CustomPage,
                IndieDBLink = entity.IndieDBLink,
                Color = entity.Color,
                BackgroundColor = entity.BackgroundColor,
                LinkColor = entity.LinkColor,
                LinkHoverColor = entity.LinkHoverColor,
                HeaderLinkColor = entity.HeaderLinkColor,
                HeaderLinkHoverColor = entity.HeaderLinkHoverColor,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate
            };
        }

        public static List<Feature> ToModelList(this IEnumerable<Entities.Feature> collection)
        {
            List<Feature> list = new List<Feature>();
            foreach (Entities.Feature entity in collection)
            {
                list.Add(entity.AsModel());
            }
            return list;
        }
    }
}

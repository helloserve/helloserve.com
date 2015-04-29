using helloserve.com.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class FeatureRepository : Base.BaseRepository
    {
        public IQueryable<Feature> GetAll()
        {
            return Db.Features;
        }

        public Feature Get(int featureId)
        {
            return GetAll().Single(f => f.FeatureID == featureId);
        }

        public Feature Update(int featureId, string name, string description, string extended, DateTime createdDate, DateTime modifiedDate, int? headerImageId, string mediaFolder, string subdomain, string customPage, bool isMainFeature, string color, string backgroundColor, string linkColor, string linkHoverColor, string headerLinkColor, string headerLinkHoverColor)
        {
            Feature feature = Db.Features.SingleOrDefault(n => n.FeatureID == featureId);

            if (feature == null)
            {
                feature = new Feature();
                Db.Features.Add(feature);
            }

            feature.FeatureID = featureId;
            feature.Name = name;
            feature.Description = description;
            feature.ExtendedDescription = extended;
            feature.CreatedDate = createdDate;
            feature.ModifiedDate = modifiedDate;
            feature.HeaderImageID = headerImageId;
            feature.MediaFolder = mediaFolder;
            feature.Subdomain = subdomain;
            feature.CustomPage = customPage;
            feature.IsMainFeature = isMainFeature;
            feature.Color = color;
            feature.BackgroundColor = backgroundColor;
            feature.LinkColor = linkColor;
            feature.LinkHoverColor = linkHoverColor;
            feature.HeaderLinkColor = headerLinkColor;
            feature.HeaderLinkHoverColor = headerLinkHoverColor;

            Db.SaveChanges();

            return feature;
        }

    }
}

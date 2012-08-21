using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using helloserve.Common;

namespace helloserve.Web
{
    public class FeaturesModel
    {
        public List<Feature> Features { get; internal set; }

        public FeaturesModel()
        {
            Features = FeatureRepo.GetAll().OrderByDescending(f => f.ModifiedDate).ToList();
        }
    }

    public class FeatureModel
    {
        public Feature Feature { get; internal set; }
        public List<FeatureRequirementModel> Requirements { get; internal set; }
        public List<News> BlogPosts { get; internal set; }
        public List<Media> Media { get; internal set; }
        public List<RelatedLink> RelatedLinks { get; internal set; }

        public FeatureModel()
        {
        }

        public FeatureModel(int featureID)
        {
            Feature = FeatureRepo.GetByID(featureID);
            Requirements = FeatureRepo.GetRequirements(featureID).ToList();
            BlogPosts = NewsRepo.GetBlogPosts(featureID).OrderByDescending(p=>p.CreatedDate).ToList();
            Media = MediaRepo.GetMediaForFeature(featureID).OrderByDescending(m => m.MediaID).ToList();
            RelatedLinks = RelatedLinkRepo.GetFeatureLinks(featureID).ToList();
        }

        public static FeatureModel FromSubdomain(string domain)
        {
            FeatureModel model = new FeatureModel();

            model.Feature = FeatureRepo.GetBySubdomain(domain);
            model.Requirements = FeatureRepo.GetRequirements(model.Feature.FeatureID).ToList();
            model.BlogPosts = NewsRepo.GetBlogPosts(model.Feature.FeatureID).OrderByDescending(p => p.CreatedDate).ToList();
            model.Media = MediaRepo.GetMediaForFeature(model.Feature.FeatureID).OrderByDescending(m => m.MediaID).ToList();
            model.RelatedLinks = RelatedLinkRepo.GetFeatureLinks(model.Feature.FeatureID).ToList();

            return model;
        }
    }
}
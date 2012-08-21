using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Common;

namespace helloserve.Web
{
    public class AdminMenuModel
    {
        public List<SelectListItem> EditFeatureList { get; set; }
        public List<SelectListItem> EditNewsList { get; set; }

        public AdminMenuModel()
        {
            EditFeatureList = FeatureRepo.GetAll().Select(f => new SelectListItem() { Text = f.Name + ':' + f.CreatedDate.ToShortDateString(), Value = f.FeatureID.ToString() }).ToList();
            EditNewsList = NewsRepo.GetAllNews().OrderByDescending(f => f.CreatedDate).Select(f => new SelectListItem() { Text = f.Title + ':' + f.CreatedDate.ToShortDateString(), Value = f.NewsID.ToString() }).ToList();
        }
    }

    public class AdminFeatureModel
    {
        public Feature Feature;
        public List<News> BlogPosts;
        public AdminFeatureRelatedModel RelatedLinks;
        public AdminFeatureRequirementModel RequirementModel;

        public AdminFeatureModel()
        {
            Feature = FeatureRepo.GetNew();
            BlogPosts = new List<News>();
            RelatedLinks = new AdminFeatureRelatedModel(0);
            RequirementModel = new AdminFeatureRequirementModel(0);
        }

        public AdminFeatureModel(int id)
        {
            Feature = FeatureRepo.GetByID(id);
            BlogPosts = NewsRepo.GetBlogPosts(id).OrderByDescending(p => p.ModifiedDate).ToList();
            RelatedLinks = new AdminFeatureRelatedModel(id);
            RequirementModel = new AdminFeatureRequirementModel(id);
        }
    }

    public class AdminFeatureRelatedModel
    {
        public int FeatureID;
        public List<RelatedLink> RelatedLinks;

        public AdminFeatureRelatedModel(int featureID)
        {
            FeatureID = featureID;
            RelatedLinks = RelatedLinkRepo.GetFeatureLinks(featureID).ToList();
        }

        public void SaveLink(RelatedLink link)
        {
            if (link == null)
                return;

            if (!link.IsNew())
            {
                RelatedLink dbLink = RelatedLinkRepo.GetByID(link.RelatedLinkID);
                dbLink.FeatureID = link.FeatureID;
                dbLink.Description = link.Description;
                dbLink.Link = link.Link;
                dbLink.Icon = link.Icon;
                link = dbLink;
            }
            else
                RelatedLinks.Add(link);

            link.Save();
        }

        public void RemoveLink(int linkID)
        {
            RelatedLink link = RelatedLinks.Where(r => r.RelatedLinkID == linkID).SingleOrDefault();

            if (link == null)
                return;

            RelatedLinks.Remove(link);

            link.Delete();
        }
    }

    public class AdminFeatureRequirementModel
    {
        public int FeatureID;
        public List<SelectListItem> Requirements;
        public List<FeatureRequirementModel> FeatureRequirements;

        public AdminFeatureRequirementModel(int featureID)
        {
            FeatureID = featureID;
            Requirements = RequirementRepo.GetAll().OrderBy(r => r.Description).Select(r => new SelectListItem() { Text = r.Description, Value = r.RequirementID.ToString() }).ToList();
            FeatureRequirements = FeatureRepo.GetRequirements(featureID).ToList();
        }

        public void AttachRequirement(int requirementID)
        {
            Requirement r = RequirementRepo.GetByID(requirementID);
            FeatureRequirement fr = FeatureRequirements.Where(i => i.Requirement.RequirementID == requirementID).Select(i => i.FeatureRequirement).SingleOrDefault();

            if (fr == null)
            {
                fr = new FeatureRequirement()
                {
                    RequirementID = requirementID,
                    FeatureID = FeatureID
                };

                if (FeatureID != 0)
                    fr.Save();
            }

            FeatureRequirements.Add(new FeatureRequirementModel() { FeatureRequirement = fr, Requirement = r });
        }

        public void DetachRequirement(int requirementID)
        {
            Requirement r = RequirementRepo.GetByID(requirementID);
            FeatureRequirementModel frm = FeatureRequirements.Where(i => i.Requirement.RequirementID == requirementID).SingleOrDefault();
            if (frm == null)
                return;

            FeatureRequirement fr = frm.FeatureRequirement;

            if (fr == null)
                return;

            if (!fr.IsNew())
                    fr.Delete();

            FeatureRequirements.Remove(frm);
        }
    }

    public class AdminNewsModel
    {
        public int? FeatureID;
        public News News;

        public AdminNewsModel()
        {
            News = NewsRepo.GetNew(null);
        }

        public AdminNewsModel(int? featureID)
        {
            FeatureID = featureID;
            News = NewsRepo.GetNew(featureID);
        }

        public AdminNewsModel(int? featureID, int id)
        {
            News = NewsRepo.GetByID(id);
            FeatureID = News.FeatureID;
        }
    }

    public class AdminRequirementModel
    {
        public List<Requirement> Requirements;

        public AdminRequirementModel()
        {
            Requirements = RequirementRepo.GetAll().OrderBy(r => r.Description).ToList();
        }

        public void SaveRequirement(Requirement req)
        {
            if (req == null)
                return;

            if (!req.IsNew())
            {
                Requirement dbReq = RequirementRepo.GetByID(req.RequirementID);
                dbReq.Description = req.Description;
                dbReq.Link = req.Link;
                dbReq.Icon = req.Icon;
                req = dbReq;
            }
            else
                Requirements.Add(req);
            
            req.Save();
            Requirements = Requirements.OrderBy(r => r.Description).ToList();
        }

        public void RemoveRequirement(int requirementID)
        {
            Requirement req = Requirements.Where(r => r.RequirementID == requirementID).SingleOrDefault();

            if (req == null)
                return;

            RequirementRepo.Remove(req);
            Requirements.Remove(req);
        }
    }
}
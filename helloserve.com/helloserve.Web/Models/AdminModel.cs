using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Common;
using System.Configuration;
using System.IO;

namespace helloserve.Web
{
    public class AdminMenuModel
    {
        public List<SelectListItem> EditFeatureList { get; set; }
        public List<SelectListItem> EditNewsList { get; set; }
        public List<SelectListItem> EditForumList { get; set; }

        public AdminMenuModel()
        {
            EditFeatureList = FeatureRepo.GetAll().Select(f => new SelectListItem() { Text = f.Name + ':' + f.CreatedDate.ToShortDateString(), Value = f.FeatureID.ToString() }).ToList();
            EditNewsList = NewsRepo.GetAllNews().OrderByDescending(f => f.CreatedDate).Select(f => new SelectListItem() { Text = f.Title + ':' + f.CreatedDate.ToShortDateString(), Value = f.NewsID.ToString() }).ToList();
            EditForumList = ForumRepo.GetAll().Select(f => new SelectListItem() { Text = f.Name, Value = f.ForumID.ToString() }).ToList();
        }
    }

    public class AdminFeatureModel
    {
        public Feature Feature;
        public List<News> BlogPosts;
        public AdminFeatureRelatedModel RelatedLinks;
        public AdminFeatureRequirementModel RequirementModel;
        public AdminFeatureDownloadableModel DownloadableModel;

        public AdminFeatureModel()
        {
            Feature = FeatureRepo.GetNew();
            BlogPosts = new List<News>();
            RelatedLinks = new AdminFeatureRelatedModel(0);
            RequirementModel = new AdminFeatureRequirementModel(0);
            DownloadableModel = new AdminFeatureDownloadableModel(0);
        }

        public AdminFeatureModel(int id)
        {
            Feature = FeatureRepo.GetByID(id);
            BlogPosts = NewsRepo.GetBlogPosts(id).OrderByDescending(p => p.ModifiedDate).ToList();
            RelatedLinks = new AdminFeatureRelatedModel(id);
            RequirementModel = new AdminFeatureRequirementModel(id);
            DownloadableModel = new AdminFeatureDownloadableModel(id);
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

    public class AdminFeatureDownloadableModel
    {
        public int FeatureID;
        public List<SelectListItem> AvailableDownloads;
        public List<Downloadable> Downloadables;

        public AdminFeatureDownloadableModel(int featureID)
        {
            FeatureID = featureID;

            AvailableDownloads = DownloadableRepo.ScanFor(ConfigurationManager.AppSettings["DownloadsPath"]).Where(d => !d.FeatureID.HasValue).Select(d => new SelectListItem() { Text = string.Format("{0} ~{1} {2}", d.Name, d.ModifiedDate.ToShortDateString(), d.ModifiedDate.ToShortTimeString()), Value = d.DownloadableID.ToString() }).ToList();

            if (featureID == 0)
                Downloadables = new List<Downloadable>();
            else
                Downloadables = FeatureRepo.GetByID(featureID).Downloadables;
        }

        public void LinkDownloadable(int id)
        {
            Downloadable dl = DownloadableRepo.GetByID(id);
            dl.FeatureID = FeatureID;
            dl.Save();
            Downloadables.Add(dl);
            AvailableDownloads = DownloadableRepo.ScanFor(ConfigurationManager.AppSettings["DownloadsPath"]).Where(d => !d.FeatureID.HasValue).Select(d => new SelectListItem() { Text = string.Format("{0} ~{1} {2}", d.Name, d.ModifiedDate.ToShortDateString(), d.ModifiedDate.ToShortTimeString()), Value = d.DownloadableID.ToString() }).ToList();
        }

        public void UnlinkDownloabable(int id)
        {
            Downloadable dl = Downloadables.Where(d => d.DownloadableID == id).SingleOrDefault();
            if (dl != null)
            {
                dl.FeatureID = null;
                dl.Save();

                Downloadables.Remove(dl);
                AvailableDownloads = DownloadableRepo.ScanFor(ConfigurationManager.AppSettings["DownloadsPath"]).Where(d => !d.FeatureID.HasValue).Select(d => new SelectListItem() { Text = string.Format("{0} ~{1} {2}", d.Name, d.ModifiedDate.ToShortDateString(), d.ModifiedDate.ToShortTimeString()), Value = d.DownloadableID.ToString() }).ToList();
            }
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

    public class AdminFeatureMediaModel
    {
        public int FeatureID;
        public List<Media> MediaItems;
        public List<FeatureMedia> FeatureMediaLinks;

        public AdminFeatureMediaModel(int featureID)
        {
            FeatureID = featureID;
            MediaItems = MediaRepo.GetMediaForFeature(featureID).ToList();
            FeatureMediaLinks = FeatureMediaRepo.GetFeatureMediaLink(featureID).ToList();
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

    public class AdminMediaModel
    {
        public string CurrentPath;
        public Dictionary<string, string> BreadCrumbs;
        public List<Media> MediaItems;
        public Dictionary<string, string> Folders;

        internal AdminMediaModel()
        {

        }

        public AdminMediaModel(string path)
        {
            if (path == null)
                path = string.Empty;

            if (path.StartsWith("\\"))
                path = path.Substring(1, path.Length - 1);

            string basePath = ConfigurationManager.AppSettings["MediaPath"];
            string finalPath = Path.Combine(basePath, path);
            CurrentPath = path;

            if (path.Length > 1)
                path = "\\" + path;

            BreadCrumbs = new Dictionary<string, string>();
            string[] crumbs = path.Split('\\');
            string crumbPath = string.Empty;
            foreach (string crumb in crumbs)
            {
                if (crumb.Length > 0)
                    crumbPath += "\\" + crumb;
                BreadCrumbs.Add(crumb, crumbPath);
            }

            MediaItems = MediaRepo.GetInLocation(finalPath).ToList();
            DirectoryInfo[] subDirs = (new DirectoryInfo(finalPath)).GetDirectories();

            Folders = new Dictionary<string, string>();
            foreach (DirectoryInfo subDir in subDirs)
            {
                Folders.Add(subDir.Name, subDir.FullName.Replace(basePath, "").Replace("\\", @"\\"));
            }
        }

        public void RemoveMedia(int id)
        {
            Media item = MediaItems.Where(m => m.MediaID == id).SingleOrDefault();

            if (item == null)
                return;

            MediaRepo.Remove(item);
            MediaItems.Remove(item);
        }

        public static AdminMediaModel ForFeature(int featureID)
        {
            AdminMediaModel model = new AdminMediaModel()
            {
                BreadCrumbs = new Dictionary<string, string>(),
                Folders = new Dictionary<string, string>(),
                CurrentPath = string.Empty,
                MediaItems = MediaRepo.GetMediaForFeature(featureID).ToList()
            };

            return model;
        }
    }

    public class AdminForumModel
    {
        public Forum Forum;
        public List<SelectListItem> ForumCategoryList { get; set; }

        public AdminForumModel()
        {
            Forum = new Forum() { Name = "New Forum" };
            ForumCategoryList = new List<SelectListItem>();
        }

        public AdminForumModel(int id)
        {
            Forum = ForumRepo.GetByID(id);
            ForumCategoryList = Forum.Categories.ToList().Select(c => new SelectListItem() { Text = c.Name, Value = c.ForumCategoryID.ToString() }).ToList();
        }
    }

    public class AdminForumCategoryModel
    {
        public int ForumID;
        public ForumCategory Category;

        public AdminForumCategoryModel(int forumID)
        {
            ForumID = forumID;
            Category = new ForumCategory() { ForumID = forumID };
        }

        public AdminForumCategoryModel(int forumID, int forumCategoryID)
        {
            ForumID = forumID;
            Category = ForumRepo.GetCategoryByID(forumCategoryID);
        }
    }

    public class AdminUsersModel
    {
        public List<User> Users;

        public AdminUsersModel()
        {
            Users = UserRepo.GetAll().ToList();
        }
    }

    public class AdminErrorModel
    {
        public List<Error> Errors;

        public AdminErrorModel()
        {
            Errors = ErrorRepo.GetAll().OrderByDescending(e => e.ErrorDate).Take(1000).ToList();
        }
    }

    public class AdminLogFilterModel
    {
        public DateTime? Date { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public int? UserID { get; set; }
    }

    public class AdminLogModel
    {
        public List<Log> Logs;

        public AdminLogFilterModel FilterModel;
        public List<SelectListItem> Users;

        public AdminLogModel()
        {
            FilterModel = new AdminLogFilterModel();
            Logs = LogRepo.GetAll().OrderByDescending(l => l.LogDate).Take(1000).ToList();
            FillLookups();
        }

        public AdminLogModel(AdminLogFilterModel filter)
        {
            FilterModel = filter;
            Logs = LogRepo.GetAll().Where(l => ((filter.Date.HasValue && l.LogDate >= filter.Date.Value) || !filter.Date.HasValue) &&
                                              ((filter.UserID.HasValue && l.UserID == filter.UserID.Value) || !filter.UserID.HasValue) &&
                                              ((!string.IsNullOrEmpty(filter.Message) && l.Message.Contains(filter.Message)) || string.IsNullOrEmpty(filter.Message)) &&
                                              ((!string.IsNullOrEmpty(filter.Source) && l.Source.Contains(filter.Source)) || string.IsNullOrEmpty(filter.Source))).OrderByDescending(l => l.LogDate).ToList();
            FillLookups();
        }

        public void FillLookups()
        {
            if (FilterModel == null)
                Users = UserRepo.GetAll().Select(u => new SelectListItem() { Text = string.Format("{0}: {1}", u.UserID, u.Username), Value = u.UserID.ToString() }).ToList();
            else
                Users = UserRepo.GetAll().Select(u => new SelectListItem() { Text = string.Format("{0}: {1}", u.UserID, u.Username), Value = u.UserID.ToString(), Selected = (FilterModel.UserID.HasValue && u.UserID == FilterModel.UserID.Value) }).ToList();
        }
    }

    public class AdminDownloadsModel
    {
        public List<Downloadable> Downloads;

        public AdminDownloadsModel()
        {
            Downloads = DownloadableRepo.GetAll().OrderByDescending(d => d.ModifiedDate).ToList();
        }
    }
}
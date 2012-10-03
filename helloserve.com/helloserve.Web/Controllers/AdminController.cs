using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Web;
using helloserve.Common;
using System.IO;
using System.Configuration;

namespace helloserve.Web
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Admin()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.Admin - What?");

            AdminMenuModel model = new AdminMenuModel();
            return View(model);
        }

        public ActionResult AdminFunctions()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AdminFunctions - What?");

            AdminMenuModel model = new AdminMenuModel();
            return PartialView("_AdminFunctions", model);
        }

        public ActionResult AddFeature()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddFeature - What?");

            return PartialView("_Feature", new AdminFeatureModel());
        }

        public ActionResult EditFeature(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditFeature - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                return PartialView("_Feature", new AdminFeatureModel(featureID));
            }

            return null;
        }

        [ValidateInput(false)]
        public ActionResult SaveFeature(FormCollection form)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveFeature - What?");

            try
            {
                int featureID = 0;
                AdminFeatureModel model;
                if (int.TryParse(form["Feature.FeatureID"], out featureID))
                {
                    if (featureID == 0)
                        model = new AdminFeatureModel();
                    else
                        model = new AdminFeatureModel(featureID);
                }
                else
                    return ReturnJsonException(new Exception(), "Save Feature - Invalid Key");

                TryUpdateModel<Feature>(model.Feature, "Feature");

                if (!ModelState.IsValid)
                {
                    return ReturnJsonResult(true, this.RenderPartialView("_Feature", model));
                }
                else
                {
                    model.Feature.Save();
                    return View("Admin", new AdminMenuModel());
                }
            }
            catch (Exception ex)
            {
                return ReturnJsonException(ex, ex.Message);
            }
        }

        public ActionResult DeleteFeature(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DeleteFeature - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                AdminFeatureModel model = new AdminFeatureModel(featureID);
                model.Feature.Delete();
            }

            return null;
        }

        public ActionResult AddBlogPost(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddBlogPost - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                AdminNewsModel model = new AdminNewsModel(featureID);
                return PartialView("_News", model);
            }

            return null;
        }

        public ActionResult LoadFeatureBlogPosts(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveNews - What?");

            int featureID = 0;
            if (int.TryParse(id, out featureID))
            {
                AdminFeatureModel model = new AdminFeatureModel(featureID);
                return PartialView("_FeatureBlogPostList", model);
            }

            return null;
        }

        public ActionResult AddNews(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddNews - What?");

            AdminNewsModel model = new AdminNewsModel();
            return PartialView("_News", model);
        }

        public ActionResult EditNews(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditNews - What?");

            int newsID = 0;
            if (int.TryParse(id, out newsID))
            {
                AdminNewsModel model = new AdminNewsModel(null, newsID);
                return PartialView("_News", model);
            }

            return null;
        }

        public ActionResult DeleteNews(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DeleteNews - What?");

            int newsID = 0;
            if (int.TryParse(id, out newsID))
            {
                AdminNewsModel model = new AdminNewsModel(null, newsID);
                model.News.Delete();
            }

            return null;
        }

        [ValidateInput(false)]
        public ActionResult SaveNews(FormCollection form)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveNews - What?");

            try
            {
                int? featureID = null;
                int id = 0;
                if (int.TryParse(form["News.FeatureID"], out id))
                    featureID = id;

                int newsID = 0;

                AdminNewsModel model;
                if (int.TryParse(form["News.NewsID"], out newsID))
                {
                    if (newsID == 0)
                        model = new AdminNewsModel(featureID);
                    else
                        model = new AdminNewsModel(featureID, newsID);
                }
                else
                    return ReturnJsonException(new Exception(), "Save News - Invalid Key");

                TryUpdateModel<News>(model.News, "News");

                if (!ModelState.IsValid)
                {
                    return ReturnJsonResult(true, this.RenderPartialView("_News", model));
                }
                else
                {
                    model.News.Save();
                    return View("Admin", new AdminMenuModel());
                }
            }
            catch (Exception ex)
            {
                return ReturnJsonException(ex, ex.Message);
            }

        }

        public ActionResult EditRequirements()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditRequirements - What?");

            AdminRequirementModel model = new AdminRequirementModel();
            return PartialView("_Requirements", model);
        }

        public ActionResult AddRequirement(Requirement requirement)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddRequirement - What?");

            AdminRequirementModel model = new AdminRequirementModel();
            model.SaveRequirement(requirement);
            return PartialView("_Requirements", model);
        }

        public ActionResult RemoveRequirement(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddRequirement - What?");

            AdminRequirementModel model = new AdminRequirementModel();

            int reqID = 0;
            if (int.TryParse(id, out reqID))
            {
                model.RemoveRequirement(reqID);
            }

            return PartialView("_Requirements", model);
        }

        public ActionResult SaveRequirements(Requirement[] requirements)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveRequirements - What?");

            AdminRequirementModel model = new AdminRequirementModel();
            foreach (Requirement req in requirements)
            {
                model.SaveRequirement(req);
            }

            return PartialView("_Requirements", model);
        }

        public ActionResult AttachRequirement(string featureID, string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AttachRequirement - What?");

            int fID = 0;
            int rID = 0;
            if (int.TryParse(featureID, out fID))
            {
                if (int.TryParse(id, out rID))
                {
                    AdminFeatureRequirementModel model = new AdminFeatureRequirementModel(fID);
                    model.AttachRequirement(rID);

                    return PartialView("_FeatureRequirements", model);
                }
            }

            return null;
        }

        public ActionResult DetachRequirement(string featureID, string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DetachRequirement - What?");

            int fetID = 0;
            int reqID = 0;
            if (int.TryParse(featureID, out fetID))
            {
                if (int.TryParse(id, out reqID))
                {
                    AdminFeatureRequirementModel model = new AdminFeatureRequirementModel(fetID);
                    model.DetachRequirement(reqID);

                    return PartialView("_FeatureRequirements", model);
                }
            }

            return null;
        }

        public ActionResult EditDownloads()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditDownloads - What?");

            AdminDownloadsModel model = new AdminDownloadsModel();
            return PartialView("_Downloads", model);
        }

        public ActionResult AddRelated(RelatedLink related)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddRelated - What?");

            if (related == null)
                return null;

            AdminFeatureRelatedModel model = new AdminFeatureRelatedModel(related.FeatureID);
            model.SaveLink(related);
            return PartialView("_FeatureRelated", model);
        }

        public ActionResult RemoveRelated(string featureID, string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddRelated - What?");


            int fID = 0;
            if (int.TryParse(featureID, out fID))
            {
                AdminFeatureRelatedModel model = new AdminFeatureRelatedModel(fID);

                int linkID = 0;
                if (int.TryParse(id, out linkID))
                {
                    model.RemoveLink(linkID);
                }
                return PartialView("_FeatureRelated", model);
            }
            return null;
        }

        public ActionResult SaveRelated(RelatedLink[] relateds)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddRelated - What?");

            if (relateds == null || relateds.Length == 0)
                return null;

            AdminFeatureRelatedModel model = new AdminFeatureRelatedModel(relateds[0].FeatureID);
            foreach (var related in relateds)
            {
                model.SaveLink(related);
            }

            return PartialView("_FeatureRelated", model);
        }

        public ActionResult MediaPage()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.MediaPage - What?");

            return View(new AdminMediaModel(string.Empty));
        }

        public ActionResult ScanMedia()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.ScanMedia - What?");

            //perform a scan
            string path = ConfigurationManager.AppSettings["MediaPath"];
            int newCount = MediaRepo.Scan(path);

            return ReturnJsonResult(false, string.Format("Media Scan complete - found {0} new media items", newCount));
        }

        public ActionResult AddMedia(FormCollection form)
        {
            return View("Admin", new AdminMenuModel());
        }

        public ActionResult MaintainMedia(string folder)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.MaintainMedia - What?");

            AdminMediaModel model = new AdminMediaModel(folder);
            return PartialView("_MediaMaintain", model);
        }

        public ActionResult RefreshMedia(string folder)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.RefreshMedia - What?");

            AdminMediaModel model = new AdminMediaModel(folder);
            return PartialView("_MediaMaintainItems", model);
        }

        public ActionResult RemoveMedia(string id, string path)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.RemoveMedia - What?");

            int mediaID = 0;
            if (int.TryParse(id, out mediaID))
            {
                AdminMediaModel model = new AdminMediaModel(path);
                model.RemoveMedia(mediaID);

                return PartialView("_MediaMaintain", model);
            }

            ErrorRepo.LogControllerError("Could not parse media ID", "Admin.RemoveMedia");
            return null;
        }

        public ActionResult AddForum()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddForum - What?");

            return PartialView("_Forum", new AdminForumModel());
        }

        public ActionResult EditForum(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditForum - What?");

            int forumID = 0;
            if (int.TryParse(id, out forumID))
            {
                return PartialView("_Forum", new AdminForumModel(forumID));
            }

            ErrorRepo.LogControllerError("Could not parse forum ID", "Admin.EditForum");
            return null;
        }

        public ActionResult SaveForum(FormCollection form)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveForum - What?");

            int forumID = 0;
            AdminForumModel model;
            int.TryParse(form["Forum.ForumID"], out forumID);

            if (forumID == 0)
                model = new AdminForumModel();
            else
                model = new AdminForumModel(forumID);

            TryUpdateModel<Forum>(model.Forum, "Forum");
            try
            {
                model.Forum.Save();
                return ReturnJsonResult(false, this.RenderPartialView("_Forum", model));
            }
            catch (Exception ex)
            {
                ErrorRepo.LogException(ex);
                return ReturnJsonException(ex, ex.Message);
            }
        }

        public ActionResult DeleteForum(string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DeleteForum - What?");

            int forumID = 0;
            if (int.TryParse(id, out forumID))
            {
                AdminForumModel model = new AdminForumModel(forumID);
                model.Forum.Delete();
            }

            ErrorRepo.LogControllerError("Could not parse forum ID", "Admin.DeleteForum");

            return null;
        }

        public ActionResult AddForumCategory(string forum)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.AddForumCategory - What?");

            int forumID = 0;
            if (int.TryParse(forum, out forumID))
            {
                return PartialView("_ForumCategory", new AdminForumCategoryModel(forumID));
            }

            ErrorRepo.LogControllerError("Could not parse forum ID", "Admin.AddForumCategory");

            return null;
        }

        public ActionResult EditForumCategory(string forum, string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditForumCategory - What?");

            int forumID = 0;
            int forumCategoryID = 0;

            if (int.TryParse(forum, out forumID))
            {
                if (int.TryParse(id, out forumCategoryID))
                {
                    return PartialView("_ForumCategory", new AdminForumCategoryModel(forumID, forumCategoryID));
                }
            }

            ErrorRepo.LogControllerError("Could not parse forum ID or forum Category ID", "Admin.EditForumCategory");

            return null;
        }

        public ActionResult SaveForumCategory(FormCollection form)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SaveForumCategory - What?");

            int forumID = int.Parse(form["Category.ForumID"]);
            int forumCategoryID = int.Parse(form["Category.ForumCategoryID"]);

            AdminForumCategoryModel model;

            if (forumCategoryID == 0)
                model = new AdminForumCategoryModel(forumID);
            else
                model = new AdminForumCategoryModel(forumID, forumCategoryID);

            TryUpdateModel<ForumCategory>(model.Category, "Category");

            try
            {
                model.Category.Save();
                return ReturnJsonResult(false, "");
            }
            catch (Exception ex)
            {
                ErrorRepo.LogException(ex);
                return ReturnJsonException(ex, ex.Message);
            }
        }

        public ActionResult DeleteForumCategory(string forum, string id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.EditForumCategory - What?");

            int forumID = 0;
            int forumCategoryID = 0;

            if (int.TryParse(forum, out forumID))
            {
                if (int.TryParse(id, out forumCategoryID))
                {
                    AdminForumCategoryModel model = new AdminForumCategoryModel(forumID, forumCategoryID);
                    model.Category.Delete();
                }
            }

            ErrorRepo.LogControllerError("Could not parse forum ID or forum Category ID", "Admin.DeleteForumCategory");
            return null;
        }

        public ActionResult Users()
        {
            AdminUsersModel model = new AdminUsersModel();

            return PartialView("_Users", model);
        }

        public ActionResult DeleteUser(int id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.DeleteUser - What?");

            User user = UserRepo.GetByID(id);
            user.Delete();

            return Users();
        }

        public ActionResult SendActivation(int id)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SendActication - What?");

            User user = UserRepo.GetByID(id);
            try
            {
                Email.SendActivation(user);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                ErrorRepo.LogException(ex);
            }

            return Users();
        }

        public ActionResult ErrorLog()
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SendActication - What?");

            AdminErrorModel model = new AdminErrorModel();
            return PartialView("_ErrorLog", model);
        }

        [HttpPost]
        public ActionResult SystemLog(AdminLogFilterModel filter)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.SytemLog - What?");

            AdminLogModel model = new AdminLogModel(filter);
            return PartialView("_SystemLog", model);
        }

        public ActionResult LinkDownloadable(int id, int featureID)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.LinkDownloadable - What?");

            AdminFeatureDownloadableModel model = new AdminFeatureDownloadableModel(featureID);
            model.LinkDownloadable(id);
            return PartialView("_FeatureDownloads", model);
        }

        public ActionResult UnlinkDownloadable(int id, int featureID)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Admin.UnlinkDownloadable - What?");

            AdminFeatureDownloadableModel model = new AdminFeatureDownloadableModel(featureID);
            model.UnlinkDownloabable(id);
            return PartialView("_FeatureDownloads", model);
        }
    }
}

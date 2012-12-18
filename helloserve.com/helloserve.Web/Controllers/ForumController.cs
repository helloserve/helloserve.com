using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using helloserve.Common;

namespace helloserve.Web.Controllers
{
    public class ForumController : BaseController
    {
        //
        // GET: /Forum/

        public ActionResult Forum(string forum)
        {
            ForumModel model = new ForumModel(forum);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            return View(model);
        }

        public ActionResult ForumCategory(string forum, string category)
        {
            ForumCategoryModel model = new ForumCategoryModel(forum, category);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            return View(model);
        }

        public ActionResult ForumTopic(string forum, string category, string topicID, string offset, int? postID = null)
        {
            int key = Settings.EventLogger.StartPerfLog(EventLogEntry.LogForElapsed("Serving Forum topic", "Forum.ForumTopic"));
            
            int pageNumber = int.MaxValue;
            if (offset != "Last" && !string.IsNullOrEmpty(offset))
                pageNumber = int.Parse(offset);

            ForumTopicModel model = new ForumTopicModel(forum, category, int.Parse(topicID), pageNumber, postID);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            Settings.EventLogger.LogPerfLog(key);

            return View("ForumTopic", model);
        }

        public ActionResult CreateForumTopic(string forum, string category)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            ForumTopicModel model = new ForumTopicModel(forum, category);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            return View(model);
        }

        public ActionResult PostForumTopic(FormCollection form)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            string forum = form["Forum.Name"];
            string category = form["Category.Name"];

            ForumTopicModel model = new ForumTopicModel(forum, category);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            TryUpdateModel<ForumTopic>(model.Topic, "Topic");
            try
            {
                model.Topic.InitialPost(form["TopicPost"]);
                return RedirectToAction("ForumTopic", new { forum = forum, category = category, topicID = model.Topic.ForumTopicID.ToString(), page = 0 });
            }
            catch (Exception ex)
            {
                return ReturnJsonException(ex, "Eish! Problem taking your opinion into consideration");
            }
        }

        public ActionResult DeleteForumTopic(string forum, string category, int topicID)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Forum.DeleteForumTopic What?");


            ForumTopicModel model = new ForumTopicModel(forum, category, topicID);
            model.Topic.Delete();

            return RedirectToAction("ForumCategory", new { forum = forum, category = category });
        }

        public ActionResult StickyForumTopic(string forum, string category, int topicID)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Forum.StickyForumTopic What?");


            ForumTopicModel model = new ForumTopicModel(forum, category, topicID);
            model.Topic.Sticky = !model.Topic.Sticky;
            model.Topic.Save();

            return RedirectToAction("ForumCategory", new { forum = forum, category = category });
        }

        public ActionResult LockForumTopic(string forum, string category, int topicID)
        {
            if (!Settings.Current.IsAdminUser)
                throw new Exception("Forum.LockForumTopic What?");


            ForumTopicModel model = new ForumTopicModel(forum, category, topicID);
            model.Topic.Locked = !model.Topic.Locked;
            model.Topic.Save();

            return RedirectToAction("ForumCategory", new { forum = forum, category = category });
        }

        public ActionResult CreateForumPost(string forum, string category, int topicID)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            ForumPostModel model = new ForumPostModel(forum, category, topicID);

            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            return PartialView("MaintainForumPost", model);
        }

        public ActionResult EditForumPost(string forum, string category, int topicID, int postID)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            if (!Settings.Current.IsAdminUser)
                throw new Exception("Forum.EditForumPost What?");

            ForumPostModel model = new ForumPostModel(forum, category, topicID, postID);

            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            return PartialView("MaintainForumPost", model);
        }

        public ActionResult LoadForumPost(string forum, string category, int topicID, int postID)
        {
            ForumPostModel model = new ForumPostModel(forum, category, topicID, postID);
            return PartialView("_TopicPost", model);
        }

        public ActionResult DeleteForumPost(string forum, string category, int topicID, int postID)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            if (!Settings.Current.IsAdminUser)
                throw new Exception("Forum.DeletePost What?");

            ForumPostModel model = new ForumPostModel(forum, category, topicID, postID);
            model.Post.Delete();

            return ReturnJsonResult(false, model.Post.ForumPostID.ToString());
        }

        [HttpPost]
        public ActionResult PostForumPost(FormCollection form)
        {
            if (Settings.Current.User == null)
                throw new Exception("Serve says no!");

            string forum = form["Forum.Name"];
            string category = form["Category.Name"];
            int topicID = int.Parse(form["Topic.ForumTopicID"]);
            int postID = int.Parse(form["Post.ForumPostID"]);

            ForumPostModel model;
            if (postID == 0)
                model = new ForumPostModel(forum, category, topicID);
            else
                model = new ForumPostModel(forum, category, topicID, postID);

            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            TryUpdateModel<ForumPost>(model.Post, "Post");
            try
            {
                model.Post.Save();
                return PartialView("_TopicPost", model); //ForumTopic(forum, category, topicID.ToString(), "Last");
            }
            catch (Exception ex)
            {
                return ReturnJsonException(ex, ex.Message);
            }
        }

        /// <summary>
        /// Used from the home page
        /// </summary>
        /// <param name="forum"></param>
        /// <param name="category"></param>
        /// <param name="topicID"></param>
        /// <param name="postID"></param>
        /// <returns></returns>
        public ActionResult ForumPost(string forum, string category, int topicID, int postID)
        {
            ForumPostModel model = new ForumPostModel(forum, category, topicID, postID);
            if (model.Forum.Internal && !Settings.Current.IsAdminUser)
                throw new Exception("Fake ID!");

            //work out what the page number is
            int indexof = model.Topic.Posts().ToList().IndexOf(model.Post);

            int pageNumber = (int)Math.Floor(indexof / (double)Settings.Current.ForumPages);

            return ForumTopic(forum, category, topicID.ToString(), pageNumber.ToString(), postID);
        }

    }
}

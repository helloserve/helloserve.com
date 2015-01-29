using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using helloserve.Common;

namespace helloserve.Web
{
    public class ForumModel
    {
        public Forum Forum;
        public List<ForumCategoryDetailModel> Categories;

        public ForumModel(string forum)
        {
            Forum = ForumRepo.GetByName(forum);
            Categories = Forum.GetCategoriesModel();
        }
    }

    public class ForumCategoryModel
    {
        public Forum Forum;
        public ForumCategory Category;
        public List<ForumTopicDetailModel> Topics;

        public ForumCategoryModel(string forum, string category)
        {
            Forum = ForumRepo.GetByName(forum);
            Category = Forum.Categories.Where(c => c.Name == category).Single();
            Topics = Category.GetTopicsModel().ToList();
        }
    }

    public class ForumTopicModel
    {
        public Forum Forum;
        public ForumCategory Category;
        public ForumTopic Topic;
        public List<ForumPostModel> Posts;
        public ForumPostModel ViewPost;
        public int Pages;
        public int CurrentPage;

        public ForumTopicModel(string forum, string category)
        {
            Forum = ForumRepo.GetByName(forum);
            Category = Forum.Categories.Where(c => c.Name == category).Single();
            Topic = new ForumTopic()
            {
                ForumCategoryID = Category.ForumCategoryID,
                Date = DateTime.Now,
                Sticky = false,
                Name = "New Topic",
                UserID = Settings.Current.User.UserID
            };
            Posts = Topic.Posts().ToList().Select(p => new ForumPostModel() { Category = Category, Forum = Forum, Topic = Topic, Post = p }).ToList();
            Pages = 0;
            CurrentPage = 0;
        }

        public ForumTopicModel(string forum, string category, int topicID, int page = 0, int? postID = null)
        {
            Forum = ForumRepo.GetByName(forum);
            Category = Forum.Categories.Where(c => c.Name == category).Single();
            Topic = Category.Topics.Where(t => t.ForumTopicID == topicID).Single();
            Posts = Topic.Posts(page, Settings.Current.ForumPages).ToList().Select(p => new ForumPostModel() { Category = Category, Forum = Forum, Topic = Topic, Post = p }).ToList();
            if (postID.HasValue)
                ViewPost = Posts.Where(p => p.Post.ForumPostID == postID.Value).SingleOrDefault();

            int count = Topic.PostCount();
            if (count % Settings.Current.ForumPages == 0)
                Pages = count / Settings.Current.ForumPages;
            else Pages = (int)Math.Ceiling(count / (double)Settings.Current.ForumPages);
            
            if (page == int.MaxValue)
                CurrentPage = Pages - 1;
            else
                CurrentPage = page;
        }
    }

    public class ForumPostModel
    {
        public Forum Forum;
        public ForumCategory Category;
        public ForumTopic Topic;
        public ForumPost Post;

        public ForumPostModel()
        {
        }

        public ForumPostModel(string forum, string category, int topicID)
        {
            ForumTopicModel topic = new ForumTopicModel(forum, category, topicID);
            Forum = topic.Forum;
            Category = topic.Category;
            Topic = topic.Topic;
            Post = new ForumPost()
            {
                ForumTopicID = Topic.ForumTopicID,
                UserID = Settings.Current.User.UserID,
                Date = DateTime.Now,                
            };
        }

        public ForumPostModel(string forum, string category, int topicID, int postID)
        {
            ForumTopicModel topic = new ForumTopicModel(forum, category, topicID);
            Forum = topic.Forum;
            Category = topic.Category;
            Topic = topic.Topic;
            Post = Topic.Posts().Where(p => p.ForumPostID == postID).Single();
        }
    }
}
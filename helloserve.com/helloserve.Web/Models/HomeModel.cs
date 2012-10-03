using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using helloserve.Common;
using System.Configuration;

namespace helloserve.Web
{
    public class HomeModel
    {
        public List<Feature> LatestFeatures { get; internal set; }
        public List<News> LatestNews { get; internal set; }
        public List<News> LatestBlogPosts { get; internal set; }
        public List<ForumPostModel> LatestForumPosts { get; internal set; }

        public HomeModel()
        {
            LatestFeatures = FeatureRepo.GetAll().OrderByDescending(f => f.ModifiedDate).Take(5).ToList();
            LatestNews = NewsRepo.GetAllNews().OrderByDescending(n => n.ModifiedDate).Take(10).ToList();
            LatestBlogPosts = NewsRepo.GetAllBlogPosts().OrderByDescending(b => b.ModifiedDate).Take(10).ToList();

            var forumPosts = ForumRepo.GetLatestPosts();

            if (!Settings.Current.IsAdminUser)
                forumPosts = forumPosts.Where(f => !f.Forum.Internal);

            LatestForumPosts = forumPosts.Take(10).Select(f=>new ForumPostModel() { Forum = f.Forum, Category = f.Category, Topic = f.Topic, Post = f.Post }).ToList();
        }
    }

    public class HomeMediaModel
    {
        public Media Media;

        public string ThumbLocation
        {
            get
            {
                string mediaPath = ConfigurationManager.AppSettings["MediaPath"];
                string thumbPath = ConfigurationManager.AppSettings["ThumbPath"];
                return Media.Location.Replace(mediaPath, thumbPath);
            }
        }


        public HomeMediaModel(string id)
        {
            int mediaID = 0;
            if (int.TryParse(id, out mediaID))
                Media = MediaRepo.GetByID(mediaID);
            else
                Media = MediaRepo.GetByFilename(id);
        }
    }

    public class HomeDownloadModel
    {
        public Downloadable Downloadable;

        public HomeDownloadModel(string id)
        {
            int downloadID = 0;
            if (int.TryParse(id, out downloadID))
                Downloadable = DownloadableRepo.GetByID(downloadID);
            else
                Downloadable = DownloadableRepo.GetByFilename(id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using helloserve.Common;

namespace helloserve.Web
{
    public class HomeModel
    {
        public List<Feature> LatestFeatures { get; internal set; }
        public List<News> LatestNews { get; internal set; }
        public List<News> LatestBlogPosts { get; internal set; }

        public HomeModel()
        {
            LatestFeatures = FeatureRepo.GetAll().OrderByDescending(f => f.ModifiedDate).Take(5).ToList();
            LatestNews = NewsRepo.GetAllNews().OrderByDescending(n => n.ModifiedDate).Take(10).ToList();
            LatestBlogPosts = NewsRepo.GetAllBlogPosts().OrderByDescending(b => b.ModifiedDate).Take(10).ToList();
        }
    }
}
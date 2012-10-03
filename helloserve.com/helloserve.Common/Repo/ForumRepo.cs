using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace helloserve.Common
{
    public class ForumRepo : BaseRepo<Forum>
    {
        public static Forum GetByID(int id)
        {
            return DB.Forums.Where(f => f.ForumID == id).SingleOrDefault();
        }

        public static Forum GetByName(string name)
        {
            return DB.Forums.Where(f => f.Name == name).SingleOrDefault();
        }

        public static IQueryable<ForumCategory> GetCategoriesFor(int forumID)
        {
            return DB.ForumCategories.Where(f => f.ForumID == forumID);
        }

        public static ForumCategory GetCategoryByID(int forumCategoryID)
        {
            return DB.ForumCategories.Where(f => f.ForumCategoryID == forumCategoryID).SingleOrDefault();
        }

        public static IQueryable<ForumCategoryDetailModel> GetCategoriesModel(int forumID)
        {
            var data = (from f in DB.Forums
                        join fc in DB.ForumCategories on f.ForumID equals fc.ForumID
                        join fttotals in
                            (
                                from fc in DB.ForumCategories
                                join ft in DB.ForumTopics on fc.ForumCategoryID equals ft.ForumCategoryID into ftG
                                from ftLJ in ftG.DefaultIfEmpty()
                                select new { fc.ForumCategoryID, ftCount = ftG.Count() }
                            ).Distinct() on fc.ForumCategoryID equals fttotals.ForumCategoryID
                        join ftlatest in
                            (
                                from fc in DB.ForumCategories
                                join ft in DB.ForumTopics on fc.ForumCategoryID equals ft.ForumCategoryID into ftG
                                from ftLJ in ftG.DefaultIfEmpty()
                                select new { fc.ForumCategoryID, ftLJ }
                            ) on fc.ForumCategoryID equals ftlatest.ForumCategoryID into ftL
                        where f.ForumID == forumID
                        select new ForumCategoryDetailModel()
                        {
                            Category = fc,
                            TopicCount = fttotals.ftCount,
                            LatestTopic = ftL.OrderByDescending(ft => ft.ftLJ.Date).Select(ft => ft.ftLJ).FirstOrDefault()
                        });

            return data;
        }

        public static IQueryable<ForumTopic> GetTopicsFor(int categoryID)
        {
            return DB.ForumTopics.Where(t => t.ForumCategoryID == categoryID);
        }

        public static IQueryable<ForumTopicDetailModel> GetTopicsModel(int forumCategoryID)
        {
            var data = (from fc in DB.ForumCategories
                        join ft in DB.ForumTopics on fc.ForumCategoryID equals ft.ForumCategoryID
                        join fptotals in
                            (
                                from ft in DB.ForumTopics
                                join fp in DB.ForumPosts on ft.ForumTopicID equals fp.ForumTopicID into fpG
                                from fpLJ in fpG.DefaultIfEmpty()
                                select new { ft.ForumTopicID, fpCount = fpG.Count() }
                            ).Distinct() on ft.ForumTopicID equals fptotals.ForumTopicID
                        join fplatest in
                            (
                                from ft in DB.ForumTopics
                                join fp in DB.ForumPosts on ft.ForumTopicID equals fp.ForumTopicID into fpG
                                from fpLJ in fpG.DefaultIfEmpty()
                                select new { ft.ForumTopicID, fpLJ }
                            ) on ft.ForumTopicID equals fplatest.ForumTopicID into fpL
                        where fc.ForumCategoryID == forumCategoryID
                        select new ForumTopicDetailModel()
                        {
                            Topic = ft,
                            PostCount = fptotals.fpCount,
                            LatestPost = fpL.OrderByDescending(fp => fp.fpLJ.Date).Select(fp => fp.fpLJ).FirstOrDefault()
                        });

            return data;
        }

        public static IQueryable<ForumPost> GetPostsFor(int topicID)
        {
            return DB.ForumPosts.Where(p => p.ForumTopicID == topicID);
        }

        public static void CreatePost(int topicID, int userID, string text)
        {
            ForumPost post = new ForumPost()
            {
                ForumTopicID = topicID,
                Date = DateTime.Now,
                UserID = userID,
                Post = text
            };

            DB.ForumPosts.Add(post);
            DB.SaveChanges();
        }

        public static IQueryable<ForumPostDetailModel> GetLatestPosts()
        {
            var data = from f in DB.Forums
                       join fc in DB.ForumCategories on f.ForumID equals fc.ForumID
                       join ft in DB.ForumTopics on fc.ForumCategoryID equals ft.ForumCategoryID
                       join fp in DB.ForumPosts on ft.ForumTopicID equals fp.ForumTopicID
                       orderby fp.Date descending
                       select new ForumPostDetailModel()
                       {
                           Forum = f,
                           Category = fc,
                           Topic = ft,
                           Post = fp
                       };

            return data;
        }

        public static void DeleteCategories(int forumID)
        {
            List<ForumCategory> categories = (from c in DB.ForumCategories
                                              where c.ForumID == forumID
                                              select c).ToList();

            for (int i = categories.Count - 1; i >= 0; i--)
            {
                categories[i].Delete();
            }

        }

        public static void DeleteTopics(int categoryID)
        {
            List<ForumTopic> topics = (from t in DB.ForumTopics
                                       where t.ForumCategoryID == categoryID
                                       select t).ToList();

            for (int i = topics.Count - 1; i >= 0; i--)
            {
                topics[i].Delete();
            }
        }

        public static void DeletePosts(int topicID)
        {
            List<ForumPost> posts = (from p in DB.ForumPosts
                                     where p.ForumTopicID == topicID
                                     select p).ToList();

            for (int i = posts.Count - 1; i >= 0; i--)
            {
                posts[i].Delete();
            }
        }
    }
}

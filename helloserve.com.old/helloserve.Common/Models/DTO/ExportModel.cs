using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

namespace helloserve.Common
{
    public class ExportImportModel
    {
        public List<User> Users { get; set; }
        public List<News> News { get; set; }
        public List<Feature> Features { get; set; }
        public List<Requirement> Requirements { get; set; }
        public List<RelatedLink> RelatedLinks { get; set; }
        public List<Media> Media { get; set; }
        public List<FeatureMedia> FeatureMedia { get; set; }
        public List<FeatureRequirement> FeatureRequirements { get; set; }
        public List<SellingPoint> SellingPoints { get; set; }
        public List<Forum> Forums { get; set; }
        public List<ForumCategory> ForumCategories { get; set; }
        public List<ForumTopic> ForumTopics { get; set; }
        public List<ForumPost> ForumPosts { get; set; }
        public List<Downloadable> Downloadables { get; set; }

        private void LoadModel(helloserveContext context)
        {
            Users = context.Users.ToList();
            News = context.NewsItems.ToList();
            Features = context.Features.ToList();
            Requirements = context.Requirements.ToList();
            RelatedLinks = context.RealtedLinks.ToList();
            Media = context.MediaItems.ToList();
            FeatureMedia = context.FeatureMediaItems.ToList();
            FeatureRequirements = context.FeatureRequirements.ToList();
            SellingPoints = context.SellingPoints.ToList();
            Forums = context.Forums.ToList();
            ForumCategories = context.ForumCategories.ToList();
            ForumTopics = context.ForumTopics.ToList();
            ForumPosts = context.ForumPosts.ToList();
            Downloadables = context.DownloadableItems.ToList();
        }

        private void UpdateModel<T>(T source, T destination)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                object[] attribs = prop.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true);
                if (prop.CanWrite && attribs.Length == 0)
                    prop.SetValue(destination, prop.GetValue(source, null), null);
            }
        }

        private void InsertModel<T>(T model)
        {
            Type type = typeof(T);
            PropertyInfo[] props = type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                IList<CustomAttributeData> attribsData = prop.GetCustomAttributesData();
                
                object[] attribs = prop.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DatabaseGeneratedAttribute), true);
                if (prop.CanWrite && attribs.Length > 0)
                    prop.SetValue(model, 0, null);
            }
        }

        private void WriteModel(helloserveContext context)
        {
            foreach (User user in Users)
            {
                User dbUser = context.Users.Where(u => u.UserID == user.UserID).SingleOrDefault();
                if (dbUser == null)
                    dbUser = new User();

                UpdateModel<User>(user, dbUser);
                dbUser.Save();
            }
        }

        private void WriteModel<T>(helloserveContext context, List<T> content, System.Linq.Expressions.Expression<Func<T, T, bool>> where) where T : class,IEntity
        {
            var func = where. Compile();

            foreach (T jsonEntity in content)
            {
                T dbEntity = context.Set<T>().ToList().Where(e=>func(jsonEntity, e)).SingleOrDefault();
                if (dbEntity == null)
                {
                    dbEntity = jsonEntity;
                    InsertModel<T>(dbEntity);
                }
                else
                    UpdateModel<T>(jsonEntity, dbEntity);
                
                dbEntity.Save();
            }
        }

        private void ImportModel(helloserveContext context)
        {
            Dictionary<int, int> userMapping = new Dictionary<int, int>();
            WriteModel<User>(context, Users, (u, e) => u.UserID == e.UserID || u.Username == e.Username);

            foreach (User user in Users)
            {
                if (!userMapping.ContainsKey(user.UserID))
                {
                    //find the DB user
                    User dbUser = context.Users.Where(u => u.Username == user.Username).SingleOrDefault();
                    if (dbUser == null)
                        continue;

                    userMapping.Add(user.UserID, dbUser.UserID);
                }
            }

            WriteModel<News>(context, News, (n, e) => n.NewsID == e.NewsID);
            WriteModel<Feature>(context, Features, (f, e) => f.FeatureID == e.FeatureID);
            WriteModel<Requirement>(context, Requirements, (r, e) => r.RequirementID == e.RequirementID);
            WriteModel<RelatedLink>(context, RelatedLinks, (r, e) => r.RelatedLinkID == e.RelatedLinkID);
            WriteModel<Downloadable>(context, Downloadables, (d, e) => d.DownloadableID == e.DownloadableID);

            //remap the forum topics to the new user IDs.
            foreach (ForumTopic topic in ForumTopics)
            {
                if (userMapping.ContainsKey(topic.UserID))
                    topic.UserID = userMapping[topic.UserID];
            }

            //remap the forum posts to the new user IDs.
            foreach (ForumPost post in ForumPosts)
            {
                if (userMapping.ContainsKey(post.UserID))
                    post.UserID = userMapping[post.UserID];
            }

            WriteModel<Forum>(context, Forums, (f, e) => f.ForumID == e.ForumID);
            WriteModel<ForumCategory>(context, ForumCategories, (c, e) => c.ForumCategoryID == e.ForumCategoryID);
            WriteModel<ForumTopic>(context, ForumTopics, (t, e) => t.ForumTopicID == e.ForumTopicID);
            WriteModel<ForumPost>(context, ForumPosts, (p, e) => p.ForumPostID == e.ForumPostID);
        }

        public static string ExportToJSON(helloserveContext context)
        {
            ExportImportModel model = new ExportImportModel();
            model.LoadModel(context);

            MemoryStream ms = new MemoryStream();

            using (StreamWriter sw = new StreamWriter(ms))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, model);
            }

            return Encoding.Default.GetString(ms.GetBuffer());
        }

        public static void ImportFromJSON(helloserveContext context, string json)
        {
            ExportImportModel model = null;

            using (StringReader sr = new StringReader(json))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();
                model = serializer.Deserialize<ExportImportModel>(jr);
            }

            model.ImportModel(context);
        }
    }
}

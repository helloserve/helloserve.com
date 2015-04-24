﻿using helloserve.com.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Model
{
    public class News : Entities.News
    {
        public static List<News> GetAll(bool? isPublished = true)
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().Where(n => (isPublished.HasValue && n.IsPublished == isPublished) || !isPublished.HasValue).OrderByDescending(n => n.CreatedDate).ToModelList();
        }

        public static News GetLatest()
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().Where(n => n.IsPublished).OrderByDescending(n => n.CreatedDate).FirstOrDefault().AsModel();
        }

        public static News Get(int id)
        {
            NewsRepository repo = new NewsRepository();
            return repo.GetAll().Single(n => n.NewsID == id).AsModel();
        }

        public static List<News> GetForFeature(int? featureId)
        {
            if (!featureId.HasValue)
                return new List<News>();

            NewsRepository repo = new NewsRepository();
            return repo.GetAll().Where(n => n.IsPublished && n.FeatureID == featureId).ToModelList();
        }

        public static void Save(News model)
        {
            if (model.CreatedDate == DateTime.MinValue)
                model.CreatedDate = DateTime.UtcNow;

            model.ModifiedDate = DateTime.UtcNow;

            NewsRepository repo = new NewsRepository();
            Entities.News entity = repo.Update(model.NewsID, model.FeatureID, model.Title, model.Cut, model.Post, model.CreatedDate, model.ModifiedDate, model.HeaderImageID, model.IsPublished);

            model.NewsID = entity.NewsID;
        }
    }
}

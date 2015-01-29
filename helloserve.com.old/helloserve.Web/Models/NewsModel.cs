using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using helloserve.Common;

namespace helloserve.Web
{
    public class NewsModel
    {
        public News News { get; internal set; }
        public News NextNews { get; internal set; }
        public News PrevNews { get; internal set; }

        public string FeatureLink { get; internal set; }

        public NewsModel(int newsID)
        {
            News = NewsRepo.GetByID(newsID);
            NextNews = NewsRepo.GetNext(newsID);
            PrevNews = NewsRepo.GetPrev(newsID);

            if (News.FeatureID.HasValue)
            {
                Feature feature = FeatureRepo.GetByID(News.FeatureID.Value);
                FeatureLink = feature.Link;
            }
            else
                FeatureLink = null;
        }
    }
}

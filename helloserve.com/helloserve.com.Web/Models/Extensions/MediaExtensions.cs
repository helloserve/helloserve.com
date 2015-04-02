using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Models
{
    public static class MediaExtensions
    {
        public static string ImageUrl(this Model.Media media)
        {
            if (media.MediaID == 0)
                return string.Empty;

            string mediaUrl = ConfigurationManager.AppSettings["mediaLocationUrl"];
            return string.Format("/{0}{1}", mediaUrl, media.FileName);
        }
    }
}
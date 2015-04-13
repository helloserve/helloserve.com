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

            return media.FileName.ImageUrl();
        }

        public static string ImageUrl(this string filename, string mediaFolder = null)
        {
            string mediaUrl = ConfigurationManager.AppSettings["mediaLocationUrl"];
            if (string.IsNullOrEmpty(mediaFolder))
                return string.Format("/{0}{1}", mediaUrl, filename);

            return string.Format("/{0}{1}/{2}", mediaUrl, mediaFolder, filename);
        }
    }
}
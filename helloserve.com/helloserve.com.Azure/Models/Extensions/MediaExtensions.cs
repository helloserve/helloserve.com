using helloserve.com.Azure.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Models
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

        public static MediaDataModel AsDataModel(this Model.Media model)
        {
            string physicalPath = System.Web.Hosting.HostingEnvironment.MapPath(System.Web.HttpRuntime.AppDomainAppVirtualPath);
            string filename = Path.GetFileName(model.Location);
            string folder = model.Location.Replace(filename, "");
            if (folder.EndsWith(@"\"))
                folder = folder.Remove(folder.Length - 1, 1);

            return new MediaDataModel()
            {
                MediaId = model.MediaID,
                Filename = System.IO.Path.Combine(physicalPath, "content", "media", model.Location),
                Url = model.FileName.ImageUrl(folder)
            };
        }

        public static List<MediaDataModel> ToDataModelList(this List<Model.Media> list)
        {
            List<MediaDataModel> dataModelList = new List<MediaDataModel>();
            foreach (Model.Media item in list)
            {
                dataModelList.Add(item.AsDataModel());
            }
            return dataModelList;
        }
    }
}
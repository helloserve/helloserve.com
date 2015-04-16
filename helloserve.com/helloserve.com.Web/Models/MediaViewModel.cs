using helloserve.com.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace helloserve.com.Web.Models
{
    public class MediaViewModel : BaseViewModel
    {
        public List<MediaDataModel> Media { get; set; }

        public MediaViewModel()
        {
            Media = Model.Media.GetAll().ToDataModelList();

            string physicalPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(System.Web.HttpRuntime.AppDomainAppVirtualPath), "content", "media");

            string[] files = Directory.GetFiles(physicalPath);

            MediaDataModel mediaModel;
            string relativePath;
            foreach (string file in files)
            {
                mediaModel = Media.SingleOrDefault(m => m.Filename == file);

                if (mediaModel == null)
                {
                    relativePath = file.Replace(physicalPath, "");
                    if (relativePath.StartsWith(@"\"))
                        relativePath = relativePath.Remove(0,1);

                    using (Image image = Image.FromFile(file))
                    {
                        mediaModel = Model.Media.Add(relativePath, image.Width, image.Height).AsDataModel();
                        Media.Add(mediaModel);
                    }
                }
            }
        }
    }
}
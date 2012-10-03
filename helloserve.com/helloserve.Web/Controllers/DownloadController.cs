using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using helloserve.Common;

namespace helloserve.Web.Controllers
{
    public class DownloadController : BaseController
    {
        public ActionResult Index(string id)
        {
            HomeDownloadModel model = new HomeDownloadModel(id);

            if (model == null)
            {
                //return null;
                throw new Exception("Invalid Download Selection - choose a different iDevice");
            }

            string extension = Path.GetExtension(model.Downloadable.Location);
            FileStream image = new FileStream(model.Downloadable.Location, FileMode.Open);
            FileStreamResult stream = new FileStreamResult(image, string.Format("application/{0}", extension));
            stream.FileDownloadName = model.Downloadable.Name;

            LogRepo.LogForDownload(Settings.Current.GetUserID(), model.Downloadable.DownloadableID, model.Downloadable.Name, "Download.Index");

            return stream;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using helloserve.Common;

namespace helloserve.Web
{
    public class MediaController : BaseController
    {
        public ActionResult Index(string id)
        {
            HomeMediaModel model = new HomeMediaModel(id);

            if (model == null)
            {
                //return null;
                throw new Exception("Invalid Media Selection - choose a different iDevice");
            }

            string extension = Path.GetExtension(model.Media.Location);
            FileStream image = new FileStream(model.Media.Location, FileMode.Open);
            FileStreamResult stream = new FileStreamResult(image, string.Format("image/{0}", extension));

            LogRepo.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Index");

            return stream;
        }

        public ActionResult Thumb(string id)
        {
            HomeMediaModel model = new HomeMediaModel(id);

            if (model == null)
                throw new Exception("Invalid Media Selection - choose a different iDevice");

            string extension = Path.GetExtension(model.Media.Location);
            FileStream image = new FileStream(model.ThumbLocation, FileMode.Open);
            FileStreamResult stream = new FileStreamResult(image, string.Format("image/{0}", extension));

            LogRepo.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Thumb");

            return stream;
        }
    }
}

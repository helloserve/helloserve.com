﻿using System;
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
            if (id == "-1")
                return null;

            HomeMediaModel model = new HomeMediaModel(id);

            if (model == null)
            {
                //return null;
                throw new Exception("Invalid Media Selection - choose a different iDevice");
            }

            int key = Settings.EventLogger.StartPerfLog(EventLogEntry.LogForElapsed(string.Format("serving media {0}", model.Media.MediaID), "Media.Index"));

            string extension = Path.GetExtension(model.Media.Location);
            FileStream image = new FileStream(model.Media.Location, FileMode.Open);
            FileStreamResult stream = new FileStreamResult(image, string.Format("image/{0}", extension));

            Settings.EventLogger.LogPerfLog(key);

            //LogRepo.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Index");
            Settings.EventLogger.Log(EventLogEntry.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Index"));

            return stream;
        }

        public ActionResult Thumb(string id)
        {
            if (id == "-1")
                return null;

            HomeMediaModel model = new HomeMediaModel(id);

            if (model == null)
                throw new Exception("Invalid Media Selection - choose a different iDevice");

            int key = Settings.EventLogger.StartPerfLog(EventLogEntry.LogForElapsed(string.Format("serving thumb {0}", model.Media.MediaID), "Media.Thumb"));

            string extension = Path.GetExtension(model.Media.Location);
            FileStream image = new FileStream(model.ThumbLocation, FileMode.Open);
            FileStreamResult stream = new FileStreamResult(image, string.Format("image/{0}", extension));

            Settings.EventLogger.LogPerfLog(key);

            //LogRepo.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Thumb");
            Settings.EventLogger.Log(EventLogEntry.LogForMedia(Settings.Current.GetUserID(), model.Media.MediaID, model.Media.FileName, "Media.Thumb"));

            return stream;
        }

        public ActionResult Canvas(string id)
        {
            HomeCanvasModel model = new HomeCanvasModel(id);
            return Content(model.AppDefaultPage, "text/html");
        }

        public ActionResult Script(string id, string script)
        {
            HomeScriptModel model = new HomeScriptModel(id, script);
            return Content(model.Script, "text/javascript");
        }
    }
}

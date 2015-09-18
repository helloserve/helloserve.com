using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Controllers
{
    public class MediaController : BaseController
    {
        // GET: Media
        public ActionResult Index(string id)
        {            
            string directLink = string.Format("{0}content/images/placeholder-200x150.png", ViewBag.BaseUrl);

            if (!string.IsNullOrEmpty(id))
            {
            }

            return Redirect(directLink);
        }
    }
}
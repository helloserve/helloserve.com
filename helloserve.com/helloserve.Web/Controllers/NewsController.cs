using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.Web.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News(string id)
        {
            int newsID = 0;
            if (int.TryParse(id, out newsID))
            {
                NewsModel model = new NewsModel(newsID);
                return View(model);
            };

            return null;
        }
    }
}

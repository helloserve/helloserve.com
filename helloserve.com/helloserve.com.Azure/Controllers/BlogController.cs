using helloserve.com.Azure.Models;
using helloserve.com.Azure.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Azure.Controllers
{
    public class BlogController : BaseController
    {
        // GET: Blog
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                BlogViewModel model = new BlogViewModel();
                model.Load(true);
                return View(model);
            }
            else
            {
                int idValue;
                if (int.TryParse(id, out idValue))
                    return ById(idValue);
                else
                    return ByName(id);

            }
        }

        public ActionResult ById(int id)
        {
            BlogViewModel baseModel = new BlogViewModel();
            baseModel.Load();

            NewsDataModel model = baseModel.BlogPosts.GetById(id) as NewsDataModel;
            if (model == null)
                model = new NewsDataModel();
            model.Load(state: baseModel);
            model.ShowInIsolation = true;
            return View("Blog", model);
        }

        public ActionResult ByName(string name)
        {
            BlogViewModel baseModel = new BlogViewModel();
            baseModel.Load();

            NewsDataModel model = baseModel.BlogPosts.GetByName(name) as NewsDataModel;
            if (model == null)
                model = new NewsDataModel();
            model.Load(state: baseModel);
            model.ShowInIsolation = true;
            return View("Blog", model);
        }
    }
}
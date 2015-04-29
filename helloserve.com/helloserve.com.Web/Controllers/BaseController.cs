using helloserve.com.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.com.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Request.Url.Segments.Length >= 2 && Request.Url.Segments[1].Contains("helloserve"))
                ViewBag.BaseUrl = string.Format("{0}://{1}/{2}", Request.Url.Scheme, Request.Url.Host, Request.Url.Segments[1]);
            else
                ViewBag.BaseUrl = string.Format("{0}://{1}/", Request.Url.Scheme, Request.Url.Host);

            base.OnActionExecuting(filterContext);
        }

        protected virtual void SetColors(BaseViewModel model)
        {

        }
    }
}
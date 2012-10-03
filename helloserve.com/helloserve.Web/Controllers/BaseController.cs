using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace helloserve.Web
{
    public class BaseController : Controller
    {
        public bool IsAJAX()
        {
            return Request.IsAjaxRequest();
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //if (!Request.IsAuthenticated && IsAJAX())
            //    filterContext.Result = Json(new { IsError = true, Description = "Your session has come to an end. Please log in again." }, JsonRequestBehavior.AllowGet);

            base.OnAuthorization(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {           
            //OLD CODE
            //if (Settings.Current == null || (Settings.Current != null && Settings.Current.User == null))
            //    filterContext.Result = new RedirectResult("~/Account/NotUser");

            base.OnActionExecuting(filterContext);
        }

        protected ActionResult ReturnJsonException(Exception ex, string defaultErrorMessage = "An error was detected. Action cancelled.")
        {
            return Json(new
            {
                IsError = true,
                Description = defaultErrorMessage
            });
        }

        protected ActionResult ReturnJsonResult(bool IsError, string Description = "")
        {
            return Json(new
            {
                IsError = IsError,
                Description = Description
            }, JsonRequestBehavior.AllowGet);
        }

    }
}

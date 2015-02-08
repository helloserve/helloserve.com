using helloserve.com.Shedding.Api.Controllers.Base;
using helloserve.com.Shedding.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace helloserve.com.Shedding.Api.Filters
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            BaseApiController controller = actionContext.ControllerContext.Controller as BaseApiController;

            if (controller != null)
                controller.Session = ShedSession.GetSession(actionContext.Request.Headers);

            base.OnActionExecuting(actionContext);
        }        
    }
}
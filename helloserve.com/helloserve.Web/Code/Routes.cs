using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using helloserve.Common;

namespace helloserve.Web.Code
{
    public class SubdomainRoutes : RouteBase
    {
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            List<Feature> features = Settings.DB.Features.Where(f => !string.IsNullOrEmpty(f.Subdomain)).ToList();

            var url = httpContext.Request.Headers["HOST"];
            var index = url.IndexOf(".");

            if (index < 0)
                return null;

            var subDomain = url.Substring(0, index);

            if (subDomain.Length > 0 && subDomain != "www" && subDomain != "helloserve" && httpContext.Request.RawUrl.Length <= 1 && features.Where(f=>f.Subdomain.Contains(subDomain)).Count() > 0)
            {
                var routeData = new RouteData(this, new MvcRouteHandler());
                routeData.Values.Add("controller", "Feature"); //Goes to the Feature class
                routeData.Values.Add("action", "Feature"); //Goes to the Index action on the User1Controller
                routeData.Values.Add("id", subDomain);

                return routeData;
            }

            return null;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            //string url = requestContext.HttpContext.Request.Headers["HOST"];
            
            //if (values["id"] == null)
            //    url = "www.helloserve.com";
    
            //VirtualPathData path = new VirtualPathData(requestContext.RouteData.Route, url);
            //return path;
            
            return null;
        }
    }
}
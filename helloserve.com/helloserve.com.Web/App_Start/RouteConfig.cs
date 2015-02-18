using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace helloserve.com.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Feature_Name",
                url: "Feature/{name:string}",
                defaults: new { controller = "Feature", action = "ByName", name = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Feature_Id",
                url: "Feature/{id:int}",
                defaults: new { controller = "Feature", action = "ById", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

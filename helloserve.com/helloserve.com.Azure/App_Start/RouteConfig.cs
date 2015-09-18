using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace helloserve.com.Azure
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "admin/{action}/{id}",
                defaults: new { controller = "admin", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Media",
                url: "media/{id}",
                defaults: new { controller = "media", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Project",
                url: "project/{id}",
                defaults: new { controller = "project", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog",
                url: "blog/{id}",
                defaults: new { controller = "blog", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );
        }
    }
}

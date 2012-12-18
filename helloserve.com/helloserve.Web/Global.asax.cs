using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using helloserve.Common;
using helloserve.Web.Code;
using System.Configuration;

namespace helloserve.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add("Subdomains", new SubdomainRoutes());

            routes.MapRoute(
                "Media", // Route name
                "Media/{id}", // URL with parameters
                new { controller = "Media", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Thumb", // Route name
                "Thumb/{id}", // URL with parameters
                new { controller = "Media", action = "Thumb", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Download", // Route name
                "Download/{id}", // URL with parameters
                new { controller = "Download", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Canvas", // Route name
                "Canvas/{id}", // URL with parameters
                new { controller = "Media", action = "Canvas", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Script", // Route name
                "Script/{id}/{script}", // URL with parameters
                new { controller = "Media", action = "Script", id = UrlParameter.Optional, script = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Error",
                "Error/",
                new { controller = "Home", action = "Error" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            ContextFactory<helloserveContext>.GetContextHandler = () => Settings.DB;

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            UserRepo.CheckDefaultUser();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplicationTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "RouteTest",
                url: "route/test/{year}/{category}",
                defaults: new
                {
                    controller = "Home",
                    action = "RouteTest",
                    year = UrlParameter.Optional,
                    category = UrlParameter.Optional
                },
                constraints: new { year = @"\d{4}" }
            );

            routes.MapRoute(
                name: "FreeDownload",
                url: "freedownload",
                defaults: new
                {
                    controller = "Home",
                    action = "RouteTest",
                    year = 2017,
                    category = "Download"
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

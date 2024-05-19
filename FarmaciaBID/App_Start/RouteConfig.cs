using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FarmaciaBID
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Dosages",
                url: "{Dosages}/{action}/{id}",
                defaults: new { controller = "Dosages", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Patients",
                url: "{Patients}/{action}/{id}",
                defaults: new { controller = "Patients", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Measures",
                url: "{Measures}/{action}/{id}",
                defaults: new { controller = "Measures", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

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


            // Redirigir a la página de login al iniciar
            routes.MapRoute(
                name: "LoginRoute",
                url: "",
                defaults: new { controller = "Home", action = "Index" } // Cambia "Login" y "Index" según tu controlador y acción
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            // Definir rutas específicas, evitando `{Dosages}`, `{Patients}`, etc.
            routes.MapRoute(
                name: "DosagesRoute",
                url: "Dosages/{action}/{id}",
                defaults: new { controller = "Dosages", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "PatientsRoute",
                url: "Patients/{action}/{id}",
                defaults: new { controller = "Patients", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeasuresRoute",
                url: "Measures/{action}/{id}",
                defaults: new { controller = "Measures", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProceedingsRoute",
                url: "Proceedings/{action}/{id}",
                defaults: new { controller = "Proceedings", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UsersRoute",
                url: "Users/{action}/{id}",
                defaults: new { controller = "Users", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

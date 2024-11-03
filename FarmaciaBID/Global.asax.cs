using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FarmaciaBID
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null) // Verifica si la sesión no es null
            {
                if (HttpContext.Current.Session["AuthToken"] == null &&
                    !HttpContext.Current.Request.Url.AbsolutePath.Contains("/Login"))
                {
                    HttpContext.Current.Response.Redirect("~/Login/Login");
                }
            }
        }
    }
}

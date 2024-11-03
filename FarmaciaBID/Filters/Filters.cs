using System;
using System.Web;
using System.Web.Mvc;

namespace FarmaciaBID.Filters
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["AuthToken"] == null &&
                !filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Login"))
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

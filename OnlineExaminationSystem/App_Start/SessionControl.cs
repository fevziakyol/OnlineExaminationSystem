using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace OnlineExaminationSystem.App_Start
{
    public class SessionControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                FormsAuthentication.SignOut();
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "action", "login" },
                        { "controller", "login" },
                        { "returnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
                return;
            }
        }
    }
}
using MyHandsDoAnTotNghiep.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyHandsDoAnTotNghiep.Controllers
{
    public class UserBaseController : Controller
    {
        // GET: UserBase
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Personal", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
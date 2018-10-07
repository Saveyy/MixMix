using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MixMixMVC.Helpers
{
    public class AuthorizedLogin : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!AuthHelper.IsLoggedIn())
            {
                filterContext.Result = new RedirectResult("~/Customer/Login");
                return;
            }
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthHelper.IsLoggedIn();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThirdMVCStudy.Filters
{
    public class MyAuthorizeFilter:AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //判断Session里有没有登录标记
            if (httpContext.Session["LoginUser"] == null)
            {
                return false;
            }
            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //没权限，跳转到登录页
            filterContext.Result = new RedirectResult("~/Home/Login");
        }

    }
}
using LibraryDemo.App_Start;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册容器
            AutofacConfig.RegisterDependencies();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            if (ex == null) return;

            var httpEx = ex as HttpException;
            if (httpEx != null && httpEx.GetHttpCode() == 404)
            {
                Server.ClearError();
                Response.StatusCode = 404;
                Response.TrySkipIisCustomErrors = true;
                Server.Transfer("~/Views/Shared/NotFound.cshtml");
                return;
            }

            LogManager.GetCurrentClassLogger().Error(ex, "Application_Error: {0}", Request.RawUrl);
            Server.ClearError();
        }
    }
}

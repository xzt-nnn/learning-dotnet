using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ThirdMVCStudy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //开启特性路由
            routes.MapMvcAttributeRoutes();

            //路由规则越严格就写在越上面，避免路由冲突

            routes.MapRoute(
                name: "TestRouteB",
                url: "user/{id}",
                defaults: new { controller = "Home", action = "Detail" },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name:"TestRouteA",
                url:"user/{name}",
                defaults:new {controller="Home",action="TestName"}
            );



            routes.MapRoute(
                name:"UserDetail",
                url:"user/{id}",
                defaults:new {controller = "Home",action="Detail"}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

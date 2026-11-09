using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SecondWebFormApplication
{
    public class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            ////自定义路由
            //config.Routes.MapHttpRoute(
            //    name:"ActionApi",
            //    routeTemplate:"api/{controller}/{action}/{id}",
            //    defaults:new {id=RouteParameter.Optional}
            //);

            //开启路由特性
            config.MapHttpAttributeRoutes();

            //跨域配置
            //origin headers methods
            var cors = new EnableCorsAttribute("*","*","*");
            config.EnableCors(cors);

            //注册全局异常过滤器
            config.Filters.Add(new ApiExceptionFilter());  

            config.Routes.MapHttpRoute(
                name:"DefaultApi",
                //怎么把浏览器访问的URL，匹配到你写的Controller和方法
                routeTemplate:"api/{controller}/{id}",
                //标记id是可选参数
                defaults:new {id = RouteParameter.Optional}
            );
            //默认只返回json，去掉xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

    }
}
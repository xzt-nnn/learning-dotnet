using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThirdMVCStudy.Filters
{
    public class LogFilter: ActionFilterAttribute
    {

        //Action执行前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //获取请求地址、控制器、方法名
            var url = filterContext.HttpContext.Request.Url;
            var controller = filterContext.RouteData.Values["controller"];
            var action = filterContext.RouteData.Values["action"];
            Console.WriteLine($"【执行前】url：{url}，controller：{controller}，action：{action}");
            base.OnActionExecuting(filterContext);
        }

        //Action执行后
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Console.WriteLine($"【执行后】Action执行完成");
            base.OnActionExecuted(filterContext);
        }

    }
}
using LibraryDemo.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDemo.Filters
{
    public class GlobalExceptionFilter:IExceptionFilter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        //MVC发生未捕获异常时，框架自动执行这个方法
        /**
         * ExceptionContext:MVC5 异常过滤器的上下文容器，`OnException` 方法的参数，
         * 装着这次异常事件的全部信息，同时你也可以修改它来控制 MVC 后续怎么响应浏览器
         */
        public void OnException(ExceptionContext filterContext)
        {
            //这个异常已经被别的地方处理过了，无需处理，直接跳过
            if (filterContext.ExceptionHandled) return;

            var request = filterContext.HttpContext.Request;
            //写入错误日志
            logger.Error(filterContext.Exception,
                "未处理异常：{0} {1},用户：{2}",
                request.HttpMethod,
                request.RawUrl,filterContext.HttpContext.User?.Identity?.Name??"匿名");
            var user = filterContext.HttpContext.User?.Identity?.Name;
            if (string.IsNullOrWhiteSpace(user)) user = "匿名";

            //ExceptionHandled = true表示这个异常我们接管了，让MVC不要继续晚上抛、不要出黄页
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.StatusCode = 500;
            //跳过IIS自带的500错误页，用我们自定义的返回内容
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            //AJAX请求要返回Json请求->返回统一结果
            if(request.IsAjaxRequest() || IsJsonRequest(request))
            {
                filterContext.Result=new JsonResult
                {
                    Data=AjaxResult.Fail("系统繁忙，请稍后再试"),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //普通页面请求
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                };
            }

        }

        //判断客户端Accept头是否接受json
        private static bool IsJsonRequest(HttpRequestBase request)
        {
            return request.AcceptTypes != null &&
                string.Join(",", request.AcceptTypes).Contains("application/json");
        }

    }
}
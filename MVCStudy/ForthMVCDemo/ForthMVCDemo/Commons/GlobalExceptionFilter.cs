using System;
using System.Linq;
using System.Web.Mvc;

namespace ForthMVCDemo.Commons
{
    /// <summary>
    /// 全局异常过滤器：业务异常返回原始消息，其它异常统一提示系统繁忙
    /// </summary>
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        // Order 默认 -1（MVC 5 允许的最小值），且不要再注册 HandleErrorAttribute，
        // 否则两者 Order 相同、执行顺序不确定，异常可能被 HandleError 抢先处理掉

        public void OnException(ExceptionContext ctx)
        {
            if (ctx.ExceptionHandled) return;

            var ex = ctx.Exception;
            bool isBusiness = ex is BusinessException;

            // 1. 记录日志（生产环境换成 NLog / log4net）
            System.Diagnostics.Trace.TraceError(
                $"[{DateTime.Now}] {ctx.HttpContext.Request.RawUrl} | {ex}");

            // 2. 错误码与对外消息
            int code = isBusiness ? ((BusinessException)ex).Code : 500;
            string message = isBusiness ? ex.Message : "系统繁忙，请稍后再试";

            // 3. 区分 Ajax 请求和页面请求
            var request = ctx.HttpContext.Request;
            bool wantsJson = request.IsAjaxRequest()
                || (request.AcceptTypes != null
                    && request.AcceptTypes.Any(t => t.Contains("application/json")));

            if (wantsJson)
            {
                ctx.Result = new JsonNetResult(Result.Fail(message, code));
            }
            else
            {
                ctx.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new HandleErrorInfo(ex, "Unknown", "Unknown")),
                    TempData = ctx.Controller.TempData
                };
            }

            ctx.ExceptionHandled = true;
        }
    }
}

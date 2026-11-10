using Ef6MvcDemo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ef6MvcDemo.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            //标记异常已经处理，不让mvc继续走默认黄页
            filterContext.ExceptionHandled = true;
            Exception ex = filterContext.Exception;
            //throw new NotImplementedException();
            LogHelper.Error("全局捕获异常", ex);
            //判断是不是AJAX请求，AJAX返回JSON，普通页跳转错误页
            bool isAjax = filterContext.HttpContext.Request.IsAjaxRequest();
            if (isAjax)
            {
                filterContext.Result = new JsonResult
                {
                    //Data = new { code = 500, msg = "服务器异常" + ex.Message },
                    Data=ResultModel.Fail("服务器异常："+ex.Message,(int)ResultCode.ServerError),
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                //传递错误信息到错误页面
                var viewResult = new ViewResult();
                viewResult.ViewName = "Error";
                viewResult.ViewData["ErrMsg"] = ex.Message;
                viewResult.ViewData["StackTrace"] = ex.StackTrace;
                filterContext.Result = viewResult;
            }
        }
    }
}
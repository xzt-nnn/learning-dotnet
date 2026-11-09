using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThirdMVCStudy.Exceptions;

namespace ThirdMVCStudy.Filters
{
    public class MyExceptionFilter:HandleErrorAttribute
    {

        public override void OnException(ExceptionContext filterContext)
        {
            //1.获取异常对象
            Exception ex = filterContext.Exception;
            //2.标记：异常已经被我们处理了，不用向上抛出、不触发IIS默认黄页
            filterContext.ExceptionHandled = true;
            if(ex is BusinessException businessException)
            {
                Console.WriteLine($"【业务异常】code={businessException.Code},msg={businessException.Message}");
                //业务异常：返回给用户明确提示，不展示堆栈
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary()
                    {
                        ["ErrMsg"] = businessException.Message,
                        ["errCode"] = businessException.Code,
                        ["IsBusiness"] = true
                    }
                };
            }
            else
            {
                Console.WriteLine($"[异常过滤器捕获到异常]{ex.Message}");
                Console.WriteLine($"堆栈信息：{ex.StackTrace}");
                //3.返回错误视图，把异常消息传给页面
                filterContext.Result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary()
                    {
                        ["ErrMsg"] = ex.Message,
                        ["ErrStack"] = ex.StackTrace
                    }
                };

            }
            base.OnException(filterContext);
        }

    }
}
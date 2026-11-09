using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThirdMVCStudy.Filters
{
    public class ResultFilter:ActionFilterAttribute,IResultFilter
    {

        //试图、结果渲染之前
        //此时Action已经跑完，拿到Result，但是还没生成HTML返回给浏览器
        //可以修改ViewData、修改Result
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //拿到当前的Result
            var result = filterContext.Result;

            //判断是不是视图
            if(result is ViewResult viewResult)
            {
                //给视图统一加数据
                viewResult.ViewData["FromResultFilter"] = "这是结果过滤器塞进来的数据";
            }
        }

        //视图渲染完成之后，html页面已经生成，不能再修改页面内容
        //适合：统计页面渲染耗时、记录相应日志
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Console.WriteLine("页面渲染完成");
        }

    }
}
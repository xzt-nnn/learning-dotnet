using ForthMVCDemo.Commons;
using System.Web;
using System.Web.Mvc;

namespace ForthMVCDemo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // 只用自定义过滤器统一处理异常
            // HandleErrorAttribute 会争抢异常，必须不要再注册
            filters.Add(new GlobalExceptionFilter());
        }
    }
}

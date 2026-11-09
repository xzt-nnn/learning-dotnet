using System;
using System.Deployment.Internal;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net;

namespace SecondWebFormApplication
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        // 异常触发时自动执行
        override
        public void OnException(HttpActionExecutedContext context)
        {
            Exception ex = context.Exception;

            //var result = new
            //{
            //    code = 500,
            //    msg = "服务内部异常",
            //    error = ex.Message
            //};

            object res;

            HttpStatusCode code;

            if(ex is BusinessException busEx)
            {
                res = new { code = busEx.ErrCode, msg = busEx.Message };
                code = HttpStatusCode.BadRequest;
            }
            else
            {
                res = new { code = 500, msg = "服务器内部异常", error = ex.Message };
                code = HttpStatusCode.InternalServerError;
            }


            //上下文对象，里面装了本次请求、异常信息、可以用来返回响应
            context.Response = context.Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError,result);
        }
    }
}

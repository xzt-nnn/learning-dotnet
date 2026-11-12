using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ef6MvcDemo.Common;

namespace Ef6MvcDemo.Controllers
{
    //Controller是.net mvc框架提供的控制器基类
    /**
     * 提供的返回结果类型有View(),Json(),Content(),RedirectionToAction(),File(),HttpNotFound()
     * 提供的请求上下文：Request,Response(),HttpContext,Session,User,RouteData,Server
     * 数据传递：ViewData,ViewBag,TempData,ModelState
     */
    public class BaseController : Controller
    {
        //成功：返回统一 JSON
        protected JsonResult Success(object data = null, string message = "操作成功")
        {
            //Json方法通过`JavaScriptSerializer`把C#对象转化为Json对象
            //Content():不做任何处理，直接输出你传入的字符串,默认按照text/html来处理
            return Json(ResultModel.Ok(data, message),"application/json",Encoding.UTF8,JsonRequestBehavior.AllowGet);//
            //var json = new JavaScriptSerializer().Serialize(ResultModel.Ok(data));
            //return Content(json, "application/json", Encoding.UTF8);
        }

        //失败：返回统一 JSON
        protected JsonResult Fail(string message = "操作失败", int code = (int)ResultCode.Fail, object data = null)
        {
            return Json(ResultModel.Fail(message, code, data),"application/json",Encoding.UTF8, JsonRequestBehavior.AllowGet);
        }
    }
}
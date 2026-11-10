using System.Web.Mvc;
using Ef6MvcDemo.Common;

namespace Ef6MvcDemo.Controllers
{
    public class BaseController : Controller
    {
        //成功：返回统一 JSON
        protected JsonResult Success(object data = null, string message = "操作成功")
        {
            return Json(ResultModel.Ok(data, message), JsonRequestBehavior.AllowGet);
        }

        //失败：返回统一 JSON
        protected JsonResult Fail(string message = "操作失败", int code = (int)ResultCode.Fail, object data = null)
        {
            return Json(ResultModel.Fail(message, code, data), JsonRequestBehavior.AllowGet);
        }
    }
}
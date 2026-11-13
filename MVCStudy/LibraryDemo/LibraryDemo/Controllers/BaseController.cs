using System.Web.Mvc;
using LibraryDemo.Common;

namespace LibraryDemo.Controllers
{
    //Ajax接口才用的上
    public abstract class BaseController : Controller
    {
        protected JsonResult JsonOk(object data = null, string message = "操作成功")
        {
            return Json(AjaxResult.Ok(data, message), JsonRequestBehavior.AllowGet);
        }

        protected JsonResult JsonFail(string message, string code = "500")
        {
            return Json(AjaxResult.Fail(message, code), JsonRequestBehavior.AllowGet);
        }
    }
}
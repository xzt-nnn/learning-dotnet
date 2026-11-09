using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThirdMVCStudy.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            //获取上传的文件
            HttpPostedFileBase file = Request.Files["upFile"];
            if (file == null)
            {
                ViewBag.Msg = "文件不能为空";
                return View("Index");
            }

            //拿到文件名
            string fileName = file.FileName;
            //拼接保存路径
            string savePath = Path.Combine(Server.MapPath("~/Upload"), fileName);
            file.SaveAs(savePath);

            ViewBag.Msg = fileName + "文件上传成功";
            return View("Index");

        }
    }
}
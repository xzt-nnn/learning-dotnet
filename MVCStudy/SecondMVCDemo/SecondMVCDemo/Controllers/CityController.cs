﻿
using SecondMVCDemo.BLL;
using SecondMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SecondMVCDemo.Controllers
{
    public class CityController : Controller
    {

        private readonly ICityBLL bll;

        //构造方法注入
        public CityController(ICityBLL bll)
        {
            this.bll = bll;
        }

        public ActionResult deleteCity(int id)
        {
            bll.deleteCity(id);

            return RedirectToAction("CityList");
        }

        //打开编辑页，按id查出数据
        [HttpGet]
        public ActionResult updateCity(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("CityList");
            }

            City city = bll.detailCity(id.Value);
            if (city == null)
            {
                return HttpNotFound();
            }

            return View("updateCity", city);
        }

        [HttpPost]
        public ActionResult updateCity(City city)
        {
            if (!ModelState.IsValid)
            {
                return View("updateCity", city);
            }
            bll.updateCity(city);
            return RedirectToAction("CityList");


        }


        public ActionResult getDetailCity(int id)
        {
            return Json(bll.detailCity(id), JsonRequestBehavior.AllowGet);
        }
        public ActionResult detailCity(int id)
        {
            return View("Detail", bll.detailCity(id));
        }

        // GET：打开添加城市表单页
        [HttpGet]
        public ActionResult addCity()
        {
            return View("Add");
        }

        [HttpPost]
        public ActionResult addCity(City city)
        {
            // 1. 空值/校验失败，直接返回，不碰数据库
            if (city == null || !ModelState.IsValid)
            {
                return Json(new { success = false, message = "添加城市失败：省份和城市不能为空" });
            }

            // 2. 正常才走 BLL
            int rows = bll.AddCity(city);
            if (rows > 0)
            {
                return Json(new { success = true, message = "添加城市成功" });
            }

            return Json(new { success = false, message = "添加城市失败" });
        }

        public ActionResult getAllCity()
        {
            return RedirectToAction("CityList");
        }

        public ActionResult CityList(int pageIndex=1,int pageSize = 10)
        {
            return View("City", bll.getPageCity(pageIndex, pageSize));
        }

        //// GET: City
        //public ActionResult Index()
        //{

        //    var model = new City
        //    {
        //        Id = 1,
        //        Province = "广东",
        //        Name = "肇庆"
        //    };

        //    var model2 = new Student
        //    {
        //        id = 1,
        //        Name = "张三",
        //        age = 18
        //    };
        //    //ViewBag是Controller给Views传递数据的载体
        //    ViewBag.MyCityName = "肇庆";
        //    ViewBag.Province = "广东";
        //    return View("Detail"); //View()不带参数就会去找与方法同名的.cshtml的Razor页面
        //}

        //public ActionResult Test()
        //{
        //    return Content("这是Test页面，纯文本返回");
        //}

        //public ActionResult Detail()
        //{
        //    ViewBag.Title = "修改Title";
        //    return View();
        //}

        ////返回视图
        //public ActionResult Add()
        //{
        //    return View();
        //}

        ////post接受表单数据
        //[HttpPost]
        //public ActionResult Add(City city)
        //{
        //    //模型校验：检查[Required]规则
        //    if (!ModelState.IsValid)
        //    {
        //        //校验不通过，返回页面，返回错误信息和用户填的数据
        //        ViewBag.fail = "添加失败，省份和城市不能为空";
        //        return View(city);
        //    }

        //    //校验通过，写入数据库
        //    bll.AddCity(city);

        //    //添加成功后跳转到城市列表页
        //    return RedirectToAction("CityList");
        //}

    }
}
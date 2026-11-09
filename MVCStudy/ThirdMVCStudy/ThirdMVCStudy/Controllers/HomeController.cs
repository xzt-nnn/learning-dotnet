using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThirdMVCStudy.Controllers;
using ThirdMVCStudy.Exceptions;
using ThirdMVCStudy.Filters;
using ThirdMVCStudy.Models;

namespace ThirdMVCStudy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.Msg = "我是ViewBag消息";
            //ViewData["Msg2"] = "我是ViewData消息";

            return View();
        }

        public ActionResult TestRedirect()
        {
            ViewBag.Msg = "ViewBag：我要跟着跳转走！";
            ViewData["Msg2"] = "ViewData：我也要跟着跳转走！";
            TempData["Msg3"] = "TempData：我可以跟着跳转！";
            // 跳转，全新请求
            return RedirectToAction("Index");
        }

        //Get:打开新增用户表单
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        //Post:接受表单提交
        public ActionResult AddUser(User user)
        {
            //判断model校验是否通过
            if (!ModelState.IsValid)
            {
                //校验失败
                return View(user);
            }

            //模拟校验通过
            TempData["Success"] = $"用户【{user.Name}】提交成功！年龄：{user.Age}";
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            ViewBag.ReceiveId = id;
            return View();
        }

        public ActionResult TestName(string name)
        {
            ViewBag.msg = "匹配到TestName,name=" + name;
            return View();
        }

        [LogFilter]
        [Route("hello")]
        public ActionResult Hello()
        {
            return Content("hello 特性路由");
        }

        [LogFilter]
        [Route("user/{id}")]
        public ActionResult GetUser(int id)
        {
            return Content($"用户id={id}");
        }

        //登录页面，任何人都可以访问
        public ActionResult Login()
        {
            return View();
        }

        //[MyAuthorizeFilter]
        //public ActionResult AdminPage()
        //{
        //    return Content("后台页面，登录才能看");
        //}

        //模拟提交登录接口
        [HttpPost]
        public ActionResult LoginSubmit(string userName,string password)
        {
            if (userName == "admin" && password == "123456")
            {
                Session["LoginUser"] = userName;
                return RedirectToAction("AdminPage");
            }
            ViewBag.Tip = "账号或密码错误";
            return View("Login");
        }

        [MyAuthorizeFilter]
        [LogFilter]
        public ActionResult AdminPage()
        {
            ViewBag.UserName = Session["LoginUser"];
            return View();
        }

        [ResultFilter]
        public ActionResult TestResult()
        {
            ViewBag.Msg = "来自Action内部的消息";
            return View();
        }

        [MyExceptionFilter]
        public ActionResult TestError()
        {
            // 手动抛出异常，用来测试
            int a = 1;
            int b = 0;
            int c = a / b;
            return Content("正常页面");
        }

        [MyExceptionFilter]
        public ActionResult TestBusinessError()
        {
            // 模拟业务校验：用户余额不足
            bool hasMoney = false;
            if (!hasMoney)
            {
                // 抛自定义业务异常
                throw new BusinessException(1001, "余额不足，无法下单");
            }
            return Content("下单成功");
        }

        //模型修改密码，Post接口，加放CSRF标签
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePwd(string newPwd)
        {
            //走到这里，说明Token校验通过
            ViewBag.Tip = "密码修改成功：" + newPwd;
            return View();
        }

        //打开修改密码表单页面
        public ActionResult ChangePwd()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult TestReflectXss(string content)
        {
            // 直接把参数传给ViewBag
            ViewBag.Content = content;
            return View();
        }

        //存储型XSS攻击
        public ActionResult MsgBoard()
        {
            if (Session["MsgList"] == null)
            {
                Session["MsgList"] = new List<string>();
            }
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MsgBoard(string msg)
        {
            List<string> list = (List<string>)Session["MsgList"];
            list.Add(msg);
            Session["MsgList"] = list;
            return RedirectToAction("MsgBoard");
        }


        public ActionResult TestDomXss()
        {
            return View();
        }


    }
}
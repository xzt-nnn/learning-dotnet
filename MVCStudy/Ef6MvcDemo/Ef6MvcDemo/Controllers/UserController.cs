using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using Ef6MvcDemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Ef6MvcDemo.Controllers
{
    public class UserController : Controller
    {

        private IUserService _userService;

        //原来手动创建对象的方式，改为 Autofac 依赖注入后已不需要
        //private MyDbContext db = new MyDbContext();

        //public UserController()
        //{
        //    MyDbContext db = new MyDbContext();
        //    IUserRepository repo = new UserRepository(db);
        //    _userService = new UserService(repo, db);
        //}

        //构造函数注入：参数由 Autofac 容器自动传入
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View(_userService.userList());
        }

        public ActionResult GetUser(int id)
        {
            return View(_userService.GetUser(id));
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                int res = _userService.AddUser(user);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("UserName", "用户名已存在");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            User user = _userService.GetUser(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userService.EditUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            User user = _userService.GetUser(id);
            if (user == null) return HttpNotFound();
            //确认页面的视图文件叫 DeleteConfirm.cshtml
            return View("DeleteConfirm", user);
        }

        [HttpPost]
        public ActionResult DeleteConfirm(int id)
        {
            _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}
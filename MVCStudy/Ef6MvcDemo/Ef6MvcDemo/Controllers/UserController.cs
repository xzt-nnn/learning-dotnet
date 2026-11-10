using Ef6MvcDemo.Common;
using Ef6MvcDemo.Models;
using Ef6MvcDemo.Repository;
using Ef6MvcDemo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Ef6MvcDemo.Controllers
{
    public class UserController : BaseController
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
        //========== 改造前（同步） ==========
        //public ActionResult Index()
        //{
        //    return View(_userService.userList());
        //}

        //========== 改造后（异步） ==========
        public async Task<ActionResult> Index()
        {
            var list = await _userService.userList();
            return View(list);
        }

        //========== 改造前（同步） ==========
        //public ActionResult GetUser(int id)
        //{
        //    return View(_userService.GetUser(id));
        //}

        //========== 改造后（异步） ==========
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        //========== 改造前（同步） ==========
        //[HttpPost]
        //public ActionResult AddUser(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int res = _userService.AddUser(user);
        //        if (res == 1)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        ModelState.AddModelError("UserName", "用户名已存在");
        //    }
        //    return View(user);
        //}

        //========== 改造后（异步） ==========
        [HttpPost]
        public async Task<ActionResult> AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                int res = await _userService.AddUser(user);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("UserName", "用户名已存在");
            }
            return View(user);
        }

        //========== 改造前（同步） ==========
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    User user = _userService.GetUser(id);
        //    if (user == null) return HttpNotFound();
        //    return View(user);
        //}

        //========== 改造后（异步） ==========
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            User user = await _userService.GetUser(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        //========== 改造前（同步） ==========
        //[HttpPost]
        //public ActionResult Edit(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _userService.EditUser(user);
        //        return RedirectToAction("Index");
        //    }
        //    return View(user);
        //}

        //========== 改造后（异步） ==========
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //========== 改造前（同步） ==========
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    User user = _userService.GetUser(id);
        //    if (user == null) return HttpNotFound();
        //    //确认页面的视图文件叫 DeleteConfirm.cshtml
        //    return View("DeleteConfirm", user);
        //}

        //========== 改造后（异步） ==========
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await _userService.GetUser(id);
            if (user == null) return HttpNotFound();
            //确认页面的视图文件叫 DeleteConfirm.cshtml
            return View("DeleteConfirm", user);
        }

        //========== 改造前（同步） ==========
        //[HttpPost]
        //public ActionResult DeleteConfirm(int id)
        //{
        //    _userService.DeleteUser(id);
        //    return RedirectToAction("Index");
        //}

        //========== 改造后（异步） ==========
        [HttpPost]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            bool ok = await _userService.DeleteUser(id);
            if (!ok) return HttpNotFound();
            return RedirectToAction("Index");
        }

        //统一返回格式演示页面
        [HttpGet]
        public ActionResult AjaxDemo()
        {
            return View();
        }

        //ajax测试1：查询单个用户（成功/业务失败）
        //========== 改造前（同步） ==========
        //[HttpGet]
        //public ActionResult GetUserJson(int id)
        //{
        //    var user = _userService.GetUser(id);
        //    if (user == null)
        //    {
        //        return Fail("用户不存在", (int)ResultCode.NotFound);
        //    }
        //    return Success(user, "查询成功");
        //}

        //========== 改造后（异步） ==========
        [HttpGet]
        public async Task<ActionResult> GetUserJson(int id)
        {
            var user = await _userService.GetUser(id);
            if (user == null)
            {
                return Fail("用户不存在", (int)ResultCode.NotFound);
            }
            return Success(user, "查询成功");
        }

        //AJAX 测试 2：查询列表
        //========== 改造前（同步） ==========
        //[HttpGet]
        //public ActionResult UserListJson()
        //{
        //    var list = _userService.userList();
        //    return Success(list, "共 " + list.Count + " 条");
        //}

        //========== 改造后（异步） ==========
        [HttpGet]
        public async Task<ActionResult> UserListJson()
        {
            var list = await _userService.userList();
            return Success(list, "共 " + list.Count + " 条");
        }

        //AJAX 测试 3：故意抛异常，验证 GlobalExceptionFilter 的 ResultModel 返回
        [HttpGet]
        public ActionResult TestError()
        {
            throw new Exception("测试统一异常返回");
        }
    }
}

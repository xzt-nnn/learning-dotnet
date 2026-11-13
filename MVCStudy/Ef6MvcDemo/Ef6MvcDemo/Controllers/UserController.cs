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
    //未登录访问时自动跳转到 Web.config 里配置的 loginUrl
    [Authorize]
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
        //public async Task<ActionResult> Index()
        //{
        //    var list = await _userService.userList();
        //    return View(list);
        //}

        //========== 加入分页后 ==========
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10, string keyword = null)
        {
            //防止手动改 URL 传入过大的 pageSize 拖垮数据库
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var result = await _userService.UserListPagedAsync(page, pageSize, keyword);
            return View(result);
        }

        //========== 改造前（同步） ==========
        //public ActionResult GetUser(int id)
        //{
        //    return View(_userService.GetUser(id));
        //}

        //========== 改造后（异步） ==========
        //id 改为可空：URL 里没带 id 时 MVC 不会抛异常，而是走进 id == null 的分支
        public async Task<ActionResult> GetUser(int? id)
        {
            if (id == null) return HttpNotFound();

            var user = await _userService.GetUser(id.Value);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BatchDelete(List<int> ids, int page = 1, int pageSize = 10, string keyword = null)
        {
            int count = await _userService.BatchDeleteAsync(ids);

            TempData["Msg"] = count > 0
                ? string.Format("已删除 {0} 条记录", count)
                : "没有选中任何记录";

            //删完回到原来的页码和搜索条件
            return RedirectToAction("Index", new { page = page, pageSize = pageSize, keyword = keyword });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadAvatar(int id, HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0)
            {
                TempData["Msg"] = "请选择要上传的图片";
                return RedirectToAction("GetUser", new { id = id });
            }

            //物理路径由表现层提供，Service 不依赖 HttpContext
            string saveDir = Server.MapPath("~/Uploads/Avatar");

            try
            {
                await _userService.UploadAvatarAsync(id, file.InputStream, file.FileName, file.ContentLength, saveDir);
                TempData["Msg"] = "头像上传成功";
            }
            catch (ArgumentException ex)
            {
                //业务校验失败，直接把消息给用户
                TempData["Msg"] = ex.Message;
            }
            catch (Exception ex)
            {
                //系统异常记日志，不要暴露细节
                LogHelper.Error("上传头像失败", ex);
                TempData["Msg"] = "上传失败，请稍后重试";
            }

            return RedirectToAction("GetUser", new { id = id });
        }

        [HttpGet]
        public async Task<ActionResult> ExportCsv(string keyword)
        {
            byte[] bytes = await _userService.ExportUserCsvAsync(keyword);

            string fileName = "用户列表_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";

            return File(bytes, "text/csv", fileName);
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

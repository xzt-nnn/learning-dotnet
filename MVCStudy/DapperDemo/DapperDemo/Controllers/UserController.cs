using System.Web.Mvc;
using DapperDemo.Models;
using DapperDemo.Repository;

namespace DapperDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo = new UserRepository();

        public ActionResult Index()
        {
            var list = _userRepo.GetAll();
            return View(list);
        }

        // GET：打开新增表单页面
        public ActionResult Create()
        {
            return View();
        }

        // POST：提交表单
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo user)
        {
            // 模型校验（我们实体上的[Required]、[Range]在这里生效）
            if (!ModelState.IsValid)
            {
                // 校验失败，带着错误信息返回表单
                return View(user);
            }
            _userRepo.Add(user);
            // 新增成功，重定向到列表页
            return RedirectToAction("Index");
        }
    }
}

using ForthMVCDemo.Commons;
using ForthMVCDemo.Repositories;
using ForthMVCDemo.Services;
using ForthMVCDemo.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ForthMVCDemo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        // 没有 DI 容器时手动组装；有参构造留给单元测试和以后接 Autofac
        public UserController() : this(new UserService(new UserRepository()))
        {
        }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: /User/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        // POST: /User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).First().ErrorMessage;
                return new JsonNetResult(Result.Fail(error));
            }

            // 业务失败会抛 BusinessException，由 GlobalExceptionFilter 返回统一格式
            _userService.Register(vm);
            return new JsonNetResult(Result.Ok(null, "注册成功"));
        }
    }
}

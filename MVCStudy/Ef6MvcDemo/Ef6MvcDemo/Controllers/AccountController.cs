using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ef6MvcDemo.Service;

namespace Ef6MvcDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string userName, string password, string returnUrl)
        {
            //密码比对属于业务逻辑，交给 Service
            var user = await _userService.ValidateUserAsync(userName, password);

            //统一提示，不区分"用户不存在"和"密码错误"，避免被枚举账号
            if (user == null)
            {
                ModelState.AddModelError("", "用户名或密码错误");
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }

            //以下是表现层职责：构造票据、写 Cookie
            //UserData 里存角色名，Global.asax 的 PostAuthenticateRequest 会用它重建 Principal
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserName,
                System.DateTime.Now,
                System.DateTime.Now.AddMinutes(30),
                false,
                user.Role == null ? "" : user.Role.Name);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                        FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);

            //防开放重定向：只允许跳回站内地址
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "User");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}

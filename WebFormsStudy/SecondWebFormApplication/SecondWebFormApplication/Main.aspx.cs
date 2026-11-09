using System;
using System.Web.Security;

namespace SecondWebFormApplication
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // User.Identity.Name 直接拿到登录的用户名，Forms认证自带
            if (User.Identity.IsAuthenticated)
            {
                lblUser.Text = $"✅ 当前登录用户：{User.Identity.Name}";
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // 注销登录：清除认证Cookie
            FormsAuthentication.SignOut();
            // 跳回登录页
            Response.Redirect("Login.aspx");
        }
    }
}

using System;
using System.Web.Security;

namespace SecondWebFormApplication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string pwd = txtPwd.Text.Trim();

            // 这里你后续可以改成从SqlServer查询账号密码，现在先用模拟数据
            if (username == "admin" && pwd == "123456")
            {
                // 核心：写入Forms认证Cookie
                // 参数1：用户名；参数2：是否持久化（记住我）
                FormsAuthentication.SetAuthCookie(username, chkRemember.Checked);

                // 跳转到登录成功后的主页
                FormsAuthentication.RedirectFromLoginPage(username, chkRemember.Checked);
            }
            else
            {
                lblMsg.Text = "账号或密码错误！";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class LoginDemo : System.Web.UI.Page
    {

        private const string CookieName = "RememberUser";
        private const string SessionLoginKey = "LoginUser";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                HttpCookie ck = Request.Cookies[CookieName];
                if (ck != null)
                {
                    txtUserName.Text= ck.Value;
                }

                if (Session[SessionLoginKey] != null)
                {
                    lblMsg.Text = $"已经登录，用户：{Session[SessionLoginKey]}";
                }
            }
        }

        protected void btnLogin_Click(object sender,EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPwd.Text.Trim();

            if(userName == null || password == null)
            {
                lblMsg.Text = "账号或者密码不能为空";
                return;
            }

            if (userName == "admin" && password == "123456")
            {
                //写入Session、
                Session[SessionLoginKey]=userName;

                //如果勾住了记住我

                if (chkRemember.Checked)
                {
                    //写入Cookie
                    HttpCookie ck = new HttpCookie(CookieName);
                    ck.Value = userName;
                    ck.Expires.AddDays(3);
                    Response.Cookies.Add(ck);
                }
                lblMsg.Text = "登录成功";
            }
            else
            {
                lblMsg.Text = "登录失败，账号或者密码错误";
            }

        }
    }
}
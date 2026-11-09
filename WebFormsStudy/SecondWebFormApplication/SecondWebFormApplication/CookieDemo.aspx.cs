using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{

    public partial class CookieDemo : System.Web.UI.Page
    {

        private const string CookieKey = "TestCookie";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMsg.Text))
            {
                lblResult.Text = "请输入内容";
                return;
            }
            //存储到cookie
            HttpCookie ck = new HttpCookie(CookieKey);
            ck.Value= txtMsg.Text;
            ck.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Add(ck);
            lblResult.Text = "cookie写入成功";
        }

        protected void cookieRead_Click(object sender, EventArgs e)
        {
            HttpCookie ck = Request.Cookies[CookieKey];
            if (ck != null)
            {
                lblResult.Text="读取到cookie："+ck.Value;
            }
            else
            {
                lblResult.Text = "Cookie不存在或者已经过期";
            }
        }

        protected void cookieDel_Click(object sender, EventArgs e) 
        {
            HttpCookie ck = Request.Cookies[CookieKey];
            if (ck != null)
            {
                ck.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ck);
                lblResult.Text = "cookie已删除";
            }
            else
            {
                lblResult.Text = "没有找到cookie";
            }
        }
    }
}
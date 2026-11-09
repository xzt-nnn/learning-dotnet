using System;

namespace SecondWebFormApplication
{
    public partial class Target : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 优先读QueryString（Redirect方式带过来的）
                if (Request.QueryString["data"] != null)
                {
                    lblInfo.Text = "来自QueryString：" + Request.QueryString["data"];
                }
                else
                {
                    lblInfo.Text = "是Server.Transfer过来的，地址栏没变！";
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class TransferDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            // 用QueryString把参数带过去
            string msg = Server.UrlEncode(txtMsg.Text);
            Response.Redirect($"Target.aspx?data={msg}");
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            // Server.Transfer，服务器内部转到目标页
            Server.Transfer("Target.aspx");
        }
    }
}
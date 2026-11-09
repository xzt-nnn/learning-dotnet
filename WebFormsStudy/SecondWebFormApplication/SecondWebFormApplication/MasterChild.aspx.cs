using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class MasterChild : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //通过Session判断是否登录
                if (Session["LoginUser"] != null)
                {
                    //查找母版控件的Label控件
                    Label lbl = Master.FindControl("lblLoginUser") as Label;
                    if (lbl != null)
                    {
                        lbl.Text = $"欢迎{ Session["LoginUser"].ToString()}";
                    }
                }
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "母版页子页面测试成功，输入内容：" + txtTest.Text;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class Demo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //这两个变量是因为.aspx页面的asp:Button和asp:Label控件的ID属性值分别是btnClick和buttonTip，所以后台代码中可以直接使用这两个变量来操作这两个控件
            //btnClick.Text = "按钮被点击了！";
            //buttonTip.Text = "按钮点击成功！";

            //页面加载事件：每次打开页面、每次点按钮提交，都会先走这个方法
            if (!IsPostBack)
            {
                lblTip.Text = "页面初次加载";
                subTip.Text = "请输入内容";
            }
            else
            {
                //点击按钮提交后，会进到这里
                //
            }
        }

        protected void btnClick_Click(object sender, EventArgs e)
        {
            lblTip.Text = "按钮点击成功！";
            subTip.Text=$"您输入的内容是：{txtName.Text}";
        }
    }
}
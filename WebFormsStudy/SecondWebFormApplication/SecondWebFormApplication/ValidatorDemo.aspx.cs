using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class ValidatorDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // .NET 4.5+ 默认开启 Unobtrusive 校验，需要 jQuery 的 ScriptResourceMapping；
            // 本项目没引入 jQuery，这里关掉，退回传统内联脚本校验
            UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void btnSubmit_Click(object sender,EventArgs e)
        {
            if(Page.IsValid)
            {
                lblMsg.Text = "验证通过，提交成功";
            }
            else
            {
                lblMsg.Text = "验证失败，请检查输入";
            }
        }
    }
}
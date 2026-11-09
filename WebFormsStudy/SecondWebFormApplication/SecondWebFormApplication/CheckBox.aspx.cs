using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class CheckBox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender,EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("您选择的城市：");

            if(chkGuangzhou.Checked)
            {
                sb.Append("广州 ");
            }
            if(chkZhaoqing.Checked)
            {
                sb.Append("肇庆 ");
            }
            if(chkHangzhou.Checked)
            {
                sb.Append("杭州 ");
            }
            if(rdoMale.Checked)
            {
                sb.Append("性别：男");
            }
            if(rdoFemale.Checked)
            {
                sb.Append("性别：女");
            }
            lblTip.Text = sb.ToString();
        }
    }
}
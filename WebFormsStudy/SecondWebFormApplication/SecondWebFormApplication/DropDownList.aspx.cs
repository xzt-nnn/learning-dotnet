using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class DropDownListDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> cities = new List<string>
                {
                    "北京",
                    "上海",
                    "广州",
                    "深圳"
                };
                ddlCity.DataSource = cities;
                ddlCity.DataBind();
            }
            // 每次回发都重新绑定！
            //List<string> cityList = new List<string> { "广州", "肇庆", "杭州", "深圳" };
            //ddlCity.DataSource = cityList;
            //ddlCity.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblTip.Text = "您选择的城市是：" + ddlCity.SelectedValue;
        }
    }
}
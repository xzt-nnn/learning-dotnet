using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class example : System.Web.UI.Page
    {
        // 普通成员变量
        private int count
        {
            get
            {
                Console.WriteLine("进入get");
                if (ViewState["count"] == null)
                {
                    return 1;
                }
                return (int)ViewState["count"];
            }
            set
            {
                Console.WriteLine("进入set");
                ViewState["count"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            count++;
            Response.Write("当前count的值为：" + count);
        }
    }
}
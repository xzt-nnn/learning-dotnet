using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class AjaxTriggerDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnOutside_Click(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
using System;
using System.Data;
using System.Web.UI.WebControls;
using SecondWebFormApplication.BLL;

namespace SecondWebFormApplication
{
    public partial class SqlDemo : System.Web.UI.Page
    {
        private readonly CityBLL bll = new CityBLL();
        private const int PageSize = 5;
        private DataTable provinceTable;

        private int PageIndex
        {
            get { return ViewState["PageIndex"] == null ? 0 : (int)ViewState["PageIndex"]; }
            set { ViewState["PageIndex"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageIndex = 0;
                BindData();
            }
        }

        private void BindData()
        {
            // 【改动】全部交给BLL
            provinceTable = bll.GetDistinctProvince();
            int totalCount;
            DataTable dt = bll.GetCityPage(PageIndex, PageSize, out totalCount);

            int pageCount = (totalCount + PageSize - 1) / PageSize;
            if (pageCount < 1) pageCount = 1;
            if (PageIndex > pageCount - 1) PageIndex = pageCount - 1;
            if (PageIndex < 0) PageIndex = 0;

            lblPage.Text = string.Format("第{0}页/共{1}页,共{2}条", PageIndex + 1, pageCount, totalCount);
            lnkPrev.Enabled = PageIndex > 0;
            lnkNext.Enabled = PageIndex < pageCount - 1;

            gvDemo.DataSource = dt;
            gvDemo.DataBind();
        }

        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            PageIndex--;
            BindData();
        }

        protected void lnkNext_Click(object sender, EventArgs e)
        {
            PageIndex++;
            BindData();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string province = txtProvince.Text.Trim();
            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(province) || string.IsNullOrEmpty(name))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('省份和城市不能为空！')", true);
                return;
            }
            bll.AddCity(province, name);
            txtProvince.Text = "";
            txtName.Text = "";
            BindData();
        }

        protected void gvDemo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Keys[0]);
            bll.DeleteCity(id);
            BindData();
        }

        protected void gvDemo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDemo.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void gvDemo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDemo.EditIndex = -1;
            BindData();
        }

        protected void gvDemo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvDemo.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvDemo.Rows[e.RowIndex];

            DropDownList ddlPro = row.FindControl("ddlProvinceEdit") as DropDownList;
            TextBox txtCity = row.FindControl("txtNameEdit") as TextBox;
            if (ddlPro == null || txtCity == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('控件未找到')", true);
                e.Cancel = true;
                return;
            }
            string province = ddlPro.SelectedValue;
            string name = txtCity.Text.Trim();
            if (string.IsNullOrEmpty(province) || string.IsNullOrEmpty(name))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('省份和城市不能为空！')", true);
                e.Cancel = true;
                return;
            }
            bll.UpdateCity(id, province, name);
            gvDemo.EditIndex = -1;
            BindData();
        }

        protected void gvDemo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session["CityId"] = id;
                Response.Redirect("Detail.aspx");
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int id = Convert.ToInt32(btn.CommandArgument);
            Response.Redirect("Detail.aspx?id=" + id);
        }

        protected void gvDemo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            if (e.Row.RowIndex != gvDemo.EditIndex) return;

            DropDownList ddl = e.Row.FindControl("ddlProvinceEdit") as DropDownList;
            if (ddl == null) return;

            if (provinceTable != null && provinceTable.Rows.Count > 0)
            {
                ddl.DataSource = provinceTable;
                ddl.DataTextField = "Province";
                ddl.DataValueField = "Province";
                ddl.DataBind();
            }

            if (e.Row.DataItem != null)
            {
                string oldProvince = ((DataRowView)e.Row.DataItem)["Province"].ToString();
                if (ddl.Items.FindByValue(oldProvince) != null)
                    ddl.SelectedValue = oldProvince;
                else
                    ddl.Items.Insert(0, oldProvince);
            }
        }
    }
}

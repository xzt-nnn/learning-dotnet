using System;
using System.Data;
using System.Web.UI.WebControls;
using SecondWebFormApplication.BLL;

namespace SecondWebFormApplication
{
    public partial class RepeaterDemo : System.Web.UI.Page
    {
        private readonly CityBLL bll = new CityBLL();
        // 每页条数，和之前GridView保持一致
        private const int PageSize = 5;
        // 用ViewState保存当前页码，和之前写法完全相同
        private int PageIndex
        {
            get { return ViewState["PageIndex"] == null ? 0 : (int)ViewState["PageIndex"]; }
            set { ViewState["PageIndex"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRepeater();
            }
        }

        void BindRepeater()
        {
            int totalCount;
            // 调用BLL分页方法，复用你之前写好的GetCityPage
            DataTable dt = bll.GetCityPage(PageIndex, PageSize, out totalCount);
            rptCity.DataSource = dt;
            rptCity.DataBind();

            // 计算总页数
            int totalPage = (totalCount + PageSize - 1) / PageSize;
            // 页码信息展示
            lblPageInfo.Text = $"第{PageIndex + 1}页 / 共{totalPage}页，一共{totalCount}条";
            // 控制按钮禁用
            lnkPrev.Enabled = PageIndex > 0;
            lnkNext.Enabled = PageIndex < totalPage - 1;
        }

        // 上一页
        protected void lnkPrev_Click(object sender, EventArgs e)
        {
            PageIndex--;
            BindRepeater();
        }
        // 下一页
        protected void lnkNext_Click(object sender, EventArgs e)
        {
            PageIndex++;
            BindRepeater();
        }

        // 卡片里面的查看详情按钮
        protected void rptCity_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            Response.Redirect($"Detail.aspx?id={id}");
        }
    }
}

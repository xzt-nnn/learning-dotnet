using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class Detail : System.Web.UI.Page
    {

        private string connStr = ConfigurationManager.ConnectionStrings["CityConn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 优先取 QueryString 的 id，其次取 Session 里的 CityId
                int id = 0;
                bool hasId = false;

                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Convert.ToInt32(Request.QueryString["id"]);
                    hasId = true;
                }
                else if (Session["CityId"] != null)
                {
                    id = Convert.ToInt32(Session["CityId"]);
                    hasId = true;
                }

                if (hasId)
                {
                    string sql="select * from City where Id = @Id";

                    using(SqlConnection conn = new SqlConnection(connStr))
                    {
                        SqlCommand cmd = new SqlCommand(sql,conn);
                        cmd.Parameters.AddWithValue(@"Id", id);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            lblProvince.Text = dr["province"].ToString();
                            lblName.Text = dr["name"].ToString();
                        }
                        dr.Close();
                    }
                }
                else
                {
                    lblMsg.Text = "没有对应城市的Id";
                }
             
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            // 清空Session（可选）
            Session["CityId"] = null;
            Response.Redirect("SqlDemo.aspx");
        }
    }
}
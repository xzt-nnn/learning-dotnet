using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SecondWebFormApplication
{
    public partial class AjaxCity : System.Web.UI.Page
    {

        private string connStr = ConfigurationManager.ConnectionStrings["CityConn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        //新增
        protected void btnAdd_Click(object sender,EventArgs e)
        {
            string sql = "insert into City(province,name) values (@province,@name)";
            using(SqlConnection conn= new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@province", txtProvince.Text.Trim());
                cmd.Parameters.AddWithValue("@name", txtCityName.Text.Trim());
                conn.Open();
                cmd.ExecuteNonQuery();//真正执行sql
                conn.Close();
            }
            txtCityName.Text = "";
            txtProvince.Text = "";
            BindGrid();
        }

        //删除
        protected void gvCity_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(e.Keys["Id"]);
            string sql = "delete from City where Id = @id";
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id",id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            BindGrid();
        }

        //进入编辑模式
        protected void gvCity_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCity.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        //取消编辑模式
        protected void gvCity_RowCancelingEdit(object sender,GridViewCancelEditEventArgs e)
        {
            gvCity.EditIndex = -1;
            BindGrid();
        }
        

        //保存修改
        protected void gvCity_RowUpdating(object sender,GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(e.Keys["Id"]);
            string province = e.NewValues["province"].ToString();
            string cityName = e.NewValues["name"].ToString();

            string sql = "update City set province=@province,name=@name where Id=@id";

            using(SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@province", province);
                cmd.Parameters.AddWithValue("@name",cityName);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // 退出编辑状态并刷新列表，否则界面看起来"没反应"
            gvCity.EditIndex = -1;
            BindGrid();
        }

        //绑定GridView数据
        private void BindGrid()
        {
            using(SqlConnection cmd = new SqlConnection(connStr))
            {
                string sql = "select Id,province,name from City";
                SqlDataAdapter sda=new SqlDataAdapter(sql, cmd);
                DataTable dt=new DataTable();
                sda.Fill(dt);
                gvCity.DataSource = dt;
                gvCity.DataBind();
            }
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SecondWebFormApplication.Common;

namespace SecondWebFormApplication.DAL
{
    public class CityDAL
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["CityConn"].ConnectionString;

        /// <summary>
        /// 分页查询城市
        /// </summary>
        public DataTable GetCityPage(int pageIndex, int pageSize, out int totalCount)
        {
            //DataTable dt = new DataTable();
            //totalCount = 0;
            //string sqlCount = "select count(*) from City";
            //string sqlPage = @"select Id,Province,Name from City order by Id offset @skip rows fetch next @take rows only";

            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    conn.Open();
            //    // 总条数
            //    using (SqlCommand cmdCount = new SqlCommand(sqlCount, conn))
            //    {
            //        totalCount = (int)cmdCount.ExecuteScalar();
            //    }
            //    // 分页数据
            //    using (SqlCommand cmdPage = new SqlCommand(sqlPage, conn))
            //    {
            //        cmdPage.Parameters.AddWithValue("@skip", pageIndex * pageSize);
            //        cmdPage.Parameters.AddWithValue("@take", pageSize);
            //        SqlDataAdapter sda = new SqlDataAdapter(cmdPage);
            //        sda.Fill(dt);
            //    }
            //}
            //return dt;

            //查总数
            string sqlCount = "select count(*) from City";
            totalCount = (int)SqlHelper.ExecuteScalar(sqlCount);
            //分页数据
            string sqlPage = @"select Id,Province,Name from City  order by Id offset @skip rows fetch next @take rows only";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@skip",pageIndex*pageSize),
                new SqlParameter("@take",pageSize)
            };
            DataTable dt = SqlHelper.ExecuteDataTable(sqlPage, paras);
            return dt;
        }

        /// <summary>
        /// 获取所有不重复省份（给编辑下拉框用）
        /// </summary>
        public DataTable GetDistinctProvince()
        {
            //DataTable dt = new DataTable();
            //string sql = "select distinct Province from City order by Province";
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            //    sda.Fill(dt);
            //}
            //return dt;

            string sql = "select distinct Province from City order by Province";

            return SqlHelper.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 新增城市
        /// </summary>
        public bool AddCity(string province, string name)
        {
            //string sql = "insert into City(Province,Name) values(@province,@name)";
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("@province", province);
            //    cmd.Parameters.AddWithValue("@name", name);
            //    conn.Open();
            //    return cmd.ExecuteNonQuery() > 0;
            //}

            string sql="insert into City(Province,Name) values (@province,@name)";

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@province",province),
                new SqlParameter("@name",name)
            };

            return SqlHelper.ExecuteNonQuery(sql,sqlParameter) > 0;

        }

        /// <summary>
        /// 删除城市
        /// </summary>
        public bool DeleteCity(int id)
        {
            //string sql = "delete from City where Id=@id";
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("@id", id);
            //    conn.Open();
            //    return cmd.ExecuteNonQuery() > 0;
            //}

            string sql = "delete from City where Id=@Id";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            };

            return SqlHelper.ExecuteNonQuery(sql, paras) > 0;
        }

        /// <summary>
        /// 修改城市
        /// </summary>
        public bool UpdateCity(int id, string province, string name)
        {
            //string sql = "Update City set Province=@province,Name=@name where Id=@id";
            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    cmd.Parameters.AddWithValue("@province", province);
            //    cmd.Parameters.AddWithValue("@name", name);
            //    cmd.Parameters.AddWithValue("@id", id);
            //    conn.Open();
            //    return cmd.ExecuteNonQuery() > 0;
            //}

            string sql = "Update City set Province=@province,Name=@name where Id=@id";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@province",province),
                new SqlParameter("@name",name),
                new SqlParameter("@id",id)
            };

            return SqlHelper.ExecuteNonQuery(sql, paras) > 0;
        }
    }
}

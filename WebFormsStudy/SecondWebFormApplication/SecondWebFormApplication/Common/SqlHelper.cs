using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SecondWebFormApplication.Common
{
    public class SqlHelper
    {
        //读取连接字符串
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["CityConn"].ConnectionString;

        //增删改
        public static int ExecuteNonQuery(string sql,params SqlParameter[] paras)
        {
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (paras != null && paras.Length > 0)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //查询，返回DataTable
        public static DataTable ExecuteDataTable(string sql,params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (paras != null && paras.Length > 0)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
            }
            return dt;
        }

        //查询单个值
        public static object ExecuteScalar(string sql, params SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (paras != null && paras.Length > 0)
                    {
                        cmd.Parameters.AddRange(paras);
                    }
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

    }
}
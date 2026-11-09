using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DapperDemo.Helpers
{
    
    public class DbHelper
    {
        //读取配置文件中的连接字符串
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;

        //获取数据连接对象
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnStr);
        }

    }
}
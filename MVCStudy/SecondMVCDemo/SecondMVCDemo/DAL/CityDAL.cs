﻿using SecondMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SecondMVCDemo.DAL
{
    public class CityDAL
    {

        private string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["CityConn"].ConnectionString;

        //获取全部城市
        public List<City> getAllCity()
        {
            List<City> list = new List<City>();
            string sql = "select Id,Province,Name from City";
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach(DataRow row in dt.Rows)
                {
                    City c = new City();
                    c.Id = Convert.ToInt16(row["Id"]);
                    c.Province = row["Province"].ToString();
                    c.Name = row["Name"].ToString();
                    list.Add(c);
                }
            }
            return list;
        }

        //分页查询
        //需要当前的页数、当前页有多少条数据
        public List<City> getPageCity(int pageIndex,int pageSize)
        {
            List<City> cityList = new List<City>();
            int offset = (pageIndex - 1) * pageSize;
            string sql = "select Id,Province,Name from City order by Id offset @offset rows fetch next @pageSize rows only";
            using (SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                //1.给@offset、@pageSize赋值
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                //2.执行查询,
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //3.把DataTable里面的对象转换为List<City>
                foreach (DataRow row in dt.Rows)
                {
                    City city = new City();
                    city.Id = Convert.ToInt32(row["Id"]);
                    city.Province = row["Province"].ToString();
                    city.Name = row["Name"].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        //添加新城市
        public int addCity(City city)
        {
            //string sql = "insert into City(Id,Province,Name) values (@id,@province,@name)";
            string sql = "insert into City(Province,Name) values (@province,@name)";
            using (SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                //1.给sql中的占位符赋值
                cmd.Parameters.AddWithValue("@province", city.Province);
                cmd.Parameters.AddWithValue("@name", city.Name);
                //2.打开连接
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
        }

        //总数单独查
        public int getCityCount()
        {
            string sql = "select count(*) from City";
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                object count = cmd.ExecuteScalar();
                return Convert.ToInt32(count);
            }
        }

        public City detailCity(int id)
        {
            City city = new City();
            string sql = "select * from City where Id=@id";
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        city.Id = Convert.ToInt32(dr["Id"]);
                        city.Province = dr["Province"].ToString();
                        city.Name = dr["Name"].ToString();
                        return city;
                    }
                }
            }
            return new City();
        }

        public int updateCity(City city)
        {
            string sql = "update City set Province=@province,Name=@name where Id=@id";
            using(SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@province", city.Province);
                cmd.Parameters.AddWithValue("@name", city.Name);
                cmd.Parameters.AddWithValue("@id", city.Id);
                //执行
                conn.Open();
                int row = cmd.ExecuteNonQuery();
                return row;
            }
        }


        public int deleteCity(int id)
        {
            string sql = "delete from City where Id=@id";
            using (SqlConnection conn=new SqlConnection(connStr))
            {
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    int row = cmd.ExecuteNonQuery();
                    return row;
                }
            }
        }


    }
}
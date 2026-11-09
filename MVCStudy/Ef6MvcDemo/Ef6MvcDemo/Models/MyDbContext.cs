using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Models
{
    public class MyDbContext:DbContext
    {
        //构造函数：读取配置文件里的连接字符串，名字和web.config里对应
        public MyDbContext() : base("name=MyDbContext")
        {

        }

        //DbSet<T> 对应数据库中的一张表
        public DbSet<User> Users { get; set; }

    }
}
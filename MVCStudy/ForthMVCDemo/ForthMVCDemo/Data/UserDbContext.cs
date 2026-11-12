using ForthMVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ForthMVCDemo.Data
{
    public class UserDbContext:DbContext
    {

        public UserDbContext() : base("UserConn") { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<User>()
                .Property(u => u.UserName).IsRequired().HasMaxLength(50);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // 表名就是 User，不是 Users
            base.OnModelCreating(modelBuilder);
        }


    }
}
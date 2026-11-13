using System.Data.Entity;

namespace LibraryDemo.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("name=LibraryContext") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }

        //EF6的Fluent API配置，用来告诉EF：Book和Category这两张表是什么关系
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>() //我要配置Book这个实体
                .HasRequired(b => b.Category) //Book.Category是必须的
                .WithMany(c => c.Books)  //Categoty对应多本书
                .HasForeignKey(b => b.CategoryId) //CategoryId是外键列
                .WillCascadeOnDelete(false); //删除CateGory时不级联删除Book

            base.OnModelCreating(modelBuilder);
        }
    }
}
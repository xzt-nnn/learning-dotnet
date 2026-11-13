using LibraryDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace LibraryDemo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly LibraryContext _db;

        public CategoryService(LibraryContext db)
        {
            _db = db;
        }

        public IList<Category> GetAll()
        {
            //SELECT[Category].[Id], [Category].[Name], [Category].[SortOrder]
            //FROM[dbo].[Category] AS[Extent1]
            //ORDER BY[Category].[SortOrder] ASC, [Category].[Name] ASC
            return _db.Categories.AsNoTracking()
                .OrderBy(c => c.SortOrder)
                .ThenBy(c => c.Name)
                //.ToList才是真正的连接数据库
                .ToList();
        }
    }
}
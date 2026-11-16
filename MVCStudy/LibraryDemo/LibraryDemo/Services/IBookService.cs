using System.Collections.Generic;
using LibraryDemo.Common;
using LibraryDemo.Models;

namespace LibraryDemo.Services
{
    public interface IBookService
    {
        IList<Book> GetAll();
        //分页查询
        PagedResult<Book> Search(string keyword, int? categoryId, int page, int pageSize);
        //ISBN编号是否存在
        bool IsbnExists(string isbn);
        //增加图书
        void Create(Book book);
        Book GetById(int id);
        /// <summary>
        /// 判断ISBN是否存在
        /// </summary>
        /// <param name="isbn">要校验的ISBN</param>
        /// <param name="excludeId">排除的书籍Id（编辑时传当前Id，新增传0）</param>
        /// <returns>true=已经存在，false=不存在</returns>
        bool IsbnExists(string isbn, int excludeId = 0);  // 注意多了 excludeId
        void Update(Book input);

        void Delete(int id);
    }
}
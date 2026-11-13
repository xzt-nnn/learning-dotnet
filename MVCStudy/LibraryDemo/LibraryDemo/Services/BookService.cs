using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LibraryDemo.Common;
using LibraryDemo.Models;

namespace LibraryDemo.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _db;

        // 这里也不 new，由 Autofac 注入
        public BookService(LibraryContext db)
        {
            _db = db;
        }
        //判断ISBN编号是否存在
        public bool IsbnExists(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;
            var normalized = isbn.Trim();
            return _db.Books.Any(b => b.Isbn == normalized);
        }
        public void Create(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        public IList<Book> GetAll()
        {
            return _db.Books
                      .Include(b => b.Category)
                      .OrderBy(b => b.Title)
                      .ToList();
        }


        //int？：表示可以为null，传的是引用，不是值
        public PagedResult<Book> Search(string keyword, int? categoryId, int page, int pageSize)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;

            //拼接sql
            var query = _db.Books
                .Include(b => b.Category)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                query = query.Where(b => b.Title.Contains(keyword)
                || b.Author.Contains(keyword)
                || b.Isbn.Contains(keyword));
            }

            if(categoryId.HasValue && categoryId > 0)
            {
                query = query.Where(b => b.CategoryId == categoryId.Value);
            }
            //求总数
            var totalCount = query.Count();
            var totalPages = totalCount == 0 ? 0 : (int)Math.Ceiling(totalCount / (double)pageSize);

            //页越界
            if (totalPages > 0 && page > totalPages) page = totalPages;

            var items = query.OrderBy(b => b.Title)
                .ThenBy(b => b.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<Book>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public Book GetById(int id)
        {
            Book book = _db.Books.FirstOrDefault(b=>b.Id==id);
            return book;
        }

        public bool IsbnExists(string isbn, int excludeId = 0)
        {
            
        }

        public void Update(Book input)
        {
            throw new NotImplementedException();
        }
    }
}
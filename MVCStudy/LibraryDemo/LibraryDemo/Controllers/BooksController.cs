using LibraryDemo.Models;
using LibraryDemo.Services;
using LibraryDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryDemo.Controllers
{
    public class BooksController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;

        public BooksController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public ActionResult Index(string keyword,int? categoryId,int page=1)
        {
            //var items = _bookService.GetAll()
            //    .Select(b => new BookListItemVm
            //    {
            //        Id = b.Id,
            //        Isbn = b.Isbn,
            //        Title = b.Title,
            //        Author = b.Author,
            //        CategoryName = b.Category != null ? b.Category.Name : "-",
            //        Publisher = b.Publisher,
            //        AvailableCopies = b.AvailableCopies,
            //        TotalCopies = b.TotalCopies
            //    })
            //    .ToList();

            //return View(items);

            const int pageSize = 10;
            var result = _bookService.Search(keyword, categoryId, page, pageSize);

            var vm = new BookIndexVm
            {
                Keyword = keyword,
                CategoryId = categoryId,
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                Categories = BuildCategories(categoryId),
                Items = result.Items.Select(ToListItemVm).ToList()
            };

            return View(vm);
        }

        private static BookListItemVm ToListItemVm(Book b)
        {
            return new BookListItemVm
            {
                Id = b.Id,
                Isbn = b.Isbn,
                Title = b.Title,
                Author = b.Author,
                CategoryName = b.Category != null ? b.Category.Name : "-",
                Publisher = b.Publisher,
                AvailableCopies = b.AvailableCopies,
                TotalCopies = b.TotalCopies
            };
        }

        private IEnumerable<SelectListItem> BuildCategories(int? selectedId = null)
        {
            return new SelectList(_categoryService.GetAll(), "Id", "Name", selectedId);
        }

        public ActionResult Create()
        {
            return View(new BookFormVm { Categories = BuildCategories() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookFormVm vm)
        {
            // 前端校验可以被绕过，这里必须再查一次
            if (ModelState.IsValid && _bookService.IsbnExists(vm.Isbn))
            {
                ModelState.AddModelError("Isbn", "该 ISBN 已存在");
            }

            if (!ModelState.IsValid)
            {
                // POST 不会带回下拉数据，必须重新填充
                vm.Categories = BuildCategories(vm.CategoryId);
                return View(vm);
            }

            // 手动映射：只把允许的字段写入实体
            _bookService.Create(new Book
            {
                Isbn = vm.Isbn.Trim(),
                Title = vm.Title.Trim(),
                Author = vm.Author?.Trim(),
                Publisher = vm.Publisher?.Trim(),
                CategoryId = vm.CategoryId,
                TotalCopies = vm.TotalCopies,
                AvailableCopies = vm.TotalCopies,   // 新增时全部可借
                Description = vm.Description?.Trim(),
                CreatedAt = DateTime.Now
            });

            TempData["Success"] = "新增图书成功";
            return RedirectToAction("Index");
        }

        //public ActionResult TestError()
        //{
        //    throw new System.Exception("这是一条测试异常");
        //}

        //public JsonResult TestJson()
        //{
        //    return JsonOk(new { msg = "统一结果正常" });
        //}
    }
}
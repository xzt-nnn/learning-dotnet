using LibraryDemo.Models;
using LibraryDemo.Services;
using LibraryDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryDemo.Controllers
{
    public class BorrowController : Controller
    {

        private readonly IBorrowService _borrowService;
        private readonly IBookService _bookService;

        public BorrowController(IBorrowService borrowService, IBookService bookService)
        {
            _borrowService = borrowService;
            _bookService = bookService;
        }


        // GET: Borrow
        public ActionResult Create(int bookId)
        {
            var book = _bookService.GetById(bookId);
            if (book == null) return HttpNotFound();
            return View(BuildVm(book,0));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowFormVm vm)
        {
            var book = _bookService.GetById(vm.BookId);
            if (book == null) return HttpNotFound();
            if (!ModelState.IsValid)
            {
                return View(BuildVm(book, vm.ReaderId));
            }
            try
            {
                _borrowService.Borrowed(vm.BookId, vm.ReaderId);
                TempData["Success"] = "借阅成功";
            }
            catch(BusinessException e)
            {
                TempData["Error"] = e.Message;
            }
            return RedirectToAction("Index", "Books");
        }

        private object BuildVm(Book book, int readerId)
        {
            return new BorrowFormVm
            {
                BookId = book.Id,
                BookTitle = book.Title,
                BookIsbn = book.Isbn,
                TotalCopies = book.TotalCopies,
                AvailableCopies = book.AvailableCopies,
                ReaderId = readerId,
                Readers = BuildReaders()
            };
        }

        // GET: Borrow?onlyUnreturned=false
        public ActionResult Index(bool onlyUnreturned = true)
        {
            ViewBag.OnlyUnreturned = onlyUnreturned;
            return View(_borrowService.GetRecords(onlyUnreturned));
        }

        //还书
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id)
        {
            try
            {
                _borrowService.Return(id);
                TempData["Success"] = "归还成功";
            }
            catch (BusinessException e)
            {
                TempData["Error"] = e.Message;
            }
            return RedirectToAction("Index");
        }

        // 只列状态正常的读者,给下拉获取
        private IEnumerable<SelectListItem> BuildReaders()
        {
            return _borrowService.GetActiveReaders()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.CardNo + " - " + r.Name
                })
                .ToList();
        }
    }
}
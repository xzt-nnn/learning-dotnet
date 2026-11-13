using System.Collections.Generic;
using System.Web.Mvc;

namespace LibraryDemo.ViewModels
{
    //图书列表页
    public class BookIndexVm
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public IList<BookListItemVm> Items { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LibraryDemo.ViewModels
{
    // 借书表单
    public class BorrowFormVm
    {
        public int BookId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "请选择读者")]
        [Display(Name = "读者")]
        public int ReaderId { get; set; }

        // 下拉框数据，不参与提交校验
        public IEnumerable<SelectListItem> Readers { get; set; }

        // 以下只用于页面展示
        public string BookTitle { get; set; }
        public string BookIsbn { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
    }
}
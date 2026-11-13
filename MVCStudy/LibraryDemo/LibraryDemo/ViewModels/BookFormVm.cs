using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

//ViewModels是给视图用的
namespace LibraryDemo.ViewModels
{
    //增加图书表单
    public class BookFormVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入 ISBN")]
        [StringLength(20, ErrorMessage = "ISBN 最多 20 个字符")]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "请输入书名")]
        [StringLength(200, ErrorMessage = "书名最多 200 个字符")]
        [Display(Name = "书名")]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "作者")]
        public string Author { get; set; }

        [StringLength(100)]
        [Display(Name = "出版社")]
        public string Publisher { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "请选择分类")]
        [Display(Name = "分类")]
        public int CategoryId { get; set; }

        [Range(0, 9999, ErrorMessage = "馆藏总量必须在 0 到 9999 之间")]
        [Display(Name = "馆藏总量")]
        public int TotalCopies { get; set; }

        [StringLength(1000, ErrorMessage = "简介最多 1000 个字符")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "简介")]
        public string Description { get; set; }

        // 下拉框数据，不参与表单提交校验，给页面的下拉框用的
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
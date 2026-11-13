using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDemo.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "分类名不能为空")]
        [StringLength(50)]
        [Display(Name = "分类名称")]
        public string Name { get; set; }

        [Display(Name = "排序")]
        public int SortOrder { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
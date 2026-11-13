using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDemo.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }

        [Required, StringLength(20)]
        [Display(Name = "ISBN")]
        public string Isbn { get; set; }

        [Required(ErrorMessage = "书名不能为空")]
        [StringLength(200)]
        [Display(Name = "书名")]
        public string Title { get; set; }

        [StringLength(100)]
        [Display(Name = "作者")]
        public string Author { get; set; }

        [StringLength(100)]
        [Display(Name = "出版社")]
        public string Publisher { get; set; }

        [Display(Name = "分类")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Range(0, 9999)]
        [Display(Name = "馆藏总量")]
        public int TotalCopies { get; set; }

        [Range(0, 9999)]
        [Display(Name = "可借数量")]
        public int AvailableCopies { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [Display(Name = "简介")]
        public string Description { get; set; }

        [Display(Name = "入库时间")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
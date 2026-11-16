using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDemo.Models
{
    [Table("BorrowRecord")]
    public class BorrowRecord
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        public int ReaderId { get; set; }

        [ForeignKey("ReaderId")]
        public virtual Reader Reader { get; set; }

        [Display(Name = "借出时间")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "应还时间")]
        public DateTime DueDate { get; set; }

        [Display(Name = "归还时间")]
        public DateTime? ReturnDate { get; set; }

        // 0 借出，1 已还，2 逾期
        [Display(Name = "状态")]
        public byte Status { get; set; }

        [NotMapped]
        public bool IsReturned
        {
            get { return ReturnDate.HasValue; }
        }

        // 逾期只在查询时推导，不写进 Status，避免和 ReturnDate 不一致
        [NotMapped]
        public bool IsOverdue
        {
            get { return Status == 0 && DueDate < DateTime.Now; }
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryDemo.Models
{
    [Table("Reader")]
    public class Reader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "请输入读者证号")]
        [StringLength(20)]
        [Display(Name = "读者证号")]
        public string CardNo { get; set; }

        [Required(ErrorMessage = "请输入姓名")]
        [StringLength(50)]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [StringLength(20)]
        [Display(Name = "手机号")]
        public string Phone { get; set; }

        // 0 正常，1 停用，和库里的 Status tinyint 对应
        [Display(Name = "状态")]
        public byte Status { get; set; }

        [NotMapped]//表示这个字段不用映射到数据库表
        public bool IsActive
        {
            get { return Status == 0; }
            set { Status = value ? (byte)0 : (byte)1; }
        }
    }
}
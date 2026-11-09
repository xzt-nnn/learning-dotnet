using System.ComponentModel.DataAnnotations;

namespace DapperDemo.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Range(1, 120, ErrorMessage = "年龄必须在1~120之间")]
        [Display(Name = "年龄")]
        public int Age { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
    }
}

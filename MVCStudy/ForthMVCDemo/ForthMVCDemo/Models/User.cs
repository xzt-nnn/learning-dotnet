using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForthMVCDemo.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "用户名长度需为 3-50 位")]
        [Index("UX_Users_UserName", IsUnique = true)]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        // 由服务端生成，不参与表单绑定
        [Required, StringLength(256)]
        public string PasswordHash { get; set; }

        [Required, StringLength(64)]
        public string PasswordSalt { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [Index("UX_Users_Email", IsUnique = true)]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [StringLength(20)]
        [Display(Name = "手机号")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        [Required, StringLength(20)]
        public string Role { get; set; } = "User";

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}
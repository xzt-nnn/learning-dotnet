using System.ComponentModel.DataAnnotations;

namespace ForthMVCDemo.ViewModels
{
    /// <summary>
    /// 注册表单模型：只暴露允许用户填写的字段
    /// </summary>
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "用户名长度需为 3-50 位")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [Display(Name = "邮箱")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "密码长度需为 6-50 位")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        [Display(Name = "昵称")]
        public string NickName { get; set; }
    }
}

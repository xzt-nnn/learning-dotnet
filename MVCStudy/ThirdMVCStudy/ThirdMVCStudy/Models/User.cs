using System.ComponentModel.DataAnnotations;

namespace ThirdMVCStudy.Models
{
    public class User
    {
        [Required(ErrorMessage ="名字不能为空")]
        [StringLength(10,MinimumLength =2,ErrorMessage ="用户名长度必须在2~10位以内")]
        public string Name { get; set; }
        [Range(1,120,ErrorMessage ="年龄必须在1到120之间")]
        public int Age { get; set; }

        public override string ToString()
        {
            return Name + "，" + Age + "岁";
        }
    }
}

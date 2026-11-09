using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Models
{
    public class User
    {
        // EF默认：Id / 类名Id 自动识别为主键，自增
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(30)]
        public string Phone { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
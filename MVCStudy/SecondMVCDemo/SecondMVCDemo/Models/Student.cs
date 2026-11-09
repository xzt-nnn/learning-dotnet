using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondMVCDemo.Models
{
    public class Student
    {
        public int id { get; set; }

        [Required(ErrorMessage ="学生名字不能为空")]
        public string Name;

        public int age;
    }
}
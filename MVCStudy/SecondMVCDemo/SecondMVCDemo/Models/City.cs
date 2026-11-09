﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondMVCDemo.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "省份名字不能为空")]

        public string Province { get; set; }

        [Required(ErrorMessage ="城市名字不能为空")]
        public string Name { get; set; }


    }
}
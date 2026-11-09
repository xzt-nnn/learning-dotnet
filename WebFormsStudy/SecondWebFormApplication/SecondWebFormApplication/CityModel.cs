using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecondWebFormApplication
{
    public class CityModel
    {
        [Required(ErrorMessage = "省份不能为空")]
        public string Province { get; set; }

        [Required(ErrorMessage="城市名称不能为空")]
        [MinLength(2,ErrorMessage ="城市名称最少2个字")]
        [MaxLength(20,ErrorMessage ="城市名称最多20个字")]
        public string Name { get; set; }
    }
}
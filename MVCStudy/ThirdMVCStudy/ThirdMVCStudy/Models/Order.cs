using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThirdMVCStudy.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // 归属哪个用户
        public string OrderName { get; set; }
    }
}
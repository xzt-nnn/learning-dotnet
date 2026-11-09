using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondWebFormApplication
{
    public class BusinessException:Exception
    {
        public int ErrCode { get; set; }
        public BusinessException(int code, string msg) : base(msg)
        {
            ErrCode = code;
        }
    }
}
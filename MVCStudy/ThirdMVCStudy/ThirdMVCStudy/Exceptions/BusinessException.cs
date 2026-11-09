using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ThirdMVCStudy.Exceptions
{
    public class BusinessException:Exception
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(int code,string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }

        
    }
}
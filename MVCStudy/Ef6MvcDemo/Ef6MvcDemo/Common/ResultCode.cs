using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ef6MvcDemo.Common
{
    public enum ResultCode
    {
        Success=0,
        Fail=1,
        ParamError=400,
        Unauthorized=401,
        NotFound=404,
        ServerError=500
    }
}